# Chambers Technical Test

Document Management Solution

## Version
* .Net Standard 2.0.3
* .Net Core 3.1.0

## Dependencies
* NUnit 3.12.0
* NUnit3TestAdapter 3.16.1
* Moq 4.15.2
* Microsoft.EntityFrameworkCore.InMemory 3.1.10
* Microsoft.EntityFrameworkCore.SqlServer 3.1.10


## Considerations
* I haven't implemented Azure Blob Storage and just stored the pdf the MS SqlServer, was plannin on using Azure Blob Storage emulation using [Azurite](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite) but didn't due to time constraints. 


## Acceptance criteria

1. Given I have a PDF to upload
When I send the PDF to the API
Then it is uploaded successfully
>>>>> done
 
2. Given I have a non-pdf to upload
When I send the non-pdf to the API
Then the API does not accept the file and returns the appropriate messaging and status
>>>>> done
 
3. Given I have a max pdf size of 5MB
When I send the pdf to the API
Then the API does not accept the file and returns the appropriate messaging and status
>>>>> done



