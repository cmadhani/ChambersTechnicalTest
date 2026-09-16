#!/usr/bin/env bash
# Idempotent setup for the Chambers Technical Test repository.
# Installs the .NET Core 3.1 SDK plus the native libraries it needs on
# modern Ubuntu, then restores and builds the solution.
set -euo pipefail

DOTNET_CHANNEL="3.1"
DOTNET_DIR="$HOME/.dotnet"

echo "==> Installing native dependencies (libicu, libssl1.1)"
export DEBIAN_FRONTEND=noninteractive
sudo apt-get update -qq
# libicu provides globalization support for the .NET runtime.
sudo apt-get install -y -qq libicu74 || sudo apt-get install -y -qq libicu-dev

# .NET Core 3.1 links against OpenSSL 1.1, which Ubuntu 24.04 no longer ships.
if ! ldconfig -p | grep -q 'libssl.so.1.1'; then
  echo "==> libssl.so.1.1 not found; installing libssl1.1 from the Ubuntu focal archive"
  LIBSSL_DEB="libssl1.1_1.1.1f-1ubuntu2.24_amd64.deb"
  curl -fsSL -o "/tmp/${LIBSSL_DEB}" \
    "http://security.ubuntu.com/ubuntu/pool/main/o/openssl/${LIBSSL_DEB}"
  sudo dpkg -i "/tmp/${LIBSSL_DEB}"
fi

echo "==> Installing the .NET Core ${DOTNET_CHANNEL} SDK into ${DOTNET_DIR}"
if [ ! -x "${DOTNET_DIR}/dotnet" ] || ! "${DOTNET_DIR}/dotnet" --list-sdks 2>/dev/null | grep -q "^${DOTNET_CHANNEL}"; then
  curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh
  chmod +x /tmp/dotnet-install.sh
  /tmp/dotnet-install.sh --channel "${DOTNET_CHANNEL}" --install-dir "${DOTNET_DIR}"
fi

# Make the toolchain available to interactive shells (idempotent block).
MARKER="# >>> chambers dotnet env >>>"
if ! grep -qF "${MARKER}" "$HOME/.bashrc" 2>/dev/null; then
  {
    echo "${MARKER}"
    echo 'export DOTNET_ROOT="$HOME/.dotnet"'
    echo 'export PATH="$HOME/.dotnet:$PATH"'
    echo 'export DOTNET_CLI_TELEMETRY_OPTOUT=1'
    # ICU 74 (Ubuntu 24.04) is newer than .NET Core 3.1 can detect, so run
    # with globalization-invariant mode enabled.
    echo 'export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1'
    echo "# <<< chambers dotnet env <<<"
  } >> "$HOME/.bashrc"
fi

export DOTNET_ROOT="${DOTNET_DIR}"
export PATH="${DOTNET_DIR}:$PATH"
export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

echo "==> Restoring and building the solution"
cd "$(dirname "$0")/.."
dotnet build ChambersTechnicalTest.sln

echo "==> Setup complete: $(dotnet --version)"
