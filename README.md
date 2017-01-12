#Consumer Driven Contract Testing

A proof of concept project to trial the use of the .NET implementation of PACT (see https://github.com/SEEK-Jobs/pact-net).

## Installation
Tool requirements:
* MS Visual Studio 2015
 * With following extensions:
  * Specflow
  * NUnit Test Adapter

## Usage
The context for this proof of concept is online retailing. The retailer has a portal available to customers whom can retrieve the list of vouchers that have been previously awarded to the customer.

The Consumer in this case is the portal (aka the Dashboard).

The Provider in this case is the Vouchers service - a service that satisfies the request to return a collection of vouchers for a specified customer.

## Workflow
The POC is centred around the generation of a PACT file.

The tests written by the Consumer (Dashboard.ContractTests) utlimately generate the PACT file. The file defines a specific interaction between the Consumer and Provider.
These tests intract with the PACT framework to configuire a "mock" Provider - informing it of the responses to return in respect of specific requests.

Once generated, the PACT file can be "consumed" by the Provider. 

The Provider can then utilise a TDD approach (see Vouchers.ProviderTests) to build a Provider that implements behaviour necessary to return a response in respect to a given request.