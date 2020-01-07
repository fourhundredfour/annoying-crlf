# annoying-crlf
Does CRLF annoy you too? Use this Docker image to convert all files to LF.

## Index
* [Requirements](#requirements)  
* [Installation](#installation)  
* [Usage](#usage)  
* [Useful links](#useful-links)

## Requirements
* Docker

## Installation
1. Pull the image or build it by executing the following command.  
`$ docker build . -t annoying-crlf`  
2. Run the container.  
`$ docker run --rm -v $PWD:/data annoying-crlf "*.txt"`  

## Usage
* To change the lineendings, create a volume which points to the **/data** folder inside the container.  
* Pass as argument the filename which file should be converted. You can apply wildcards like:  
`$ docker run --rm -v $PWD/src:/data danielschischkin/annoying-crlf:latest "*.php" "*.txt"`  

## Useful links
* [https://hub.docker.com/r/danielschischkin/annoying-crlf](https://hub.docker.com/r/danielschischkin/annoying-crlf)