[![Build & test](https://github.com/przemek83/data-explorer-dotnet/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/przemek83/data-explorer-dotnet/actions/workflows/build-and-test.yml)
[![CodeQL](https://github.com/przemek83/data-explorer-dotnet/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/przemek83/data-explorer-dotnet/actions/workflows/github-code-scanning/codeql)
[![codecov](https://codecov.io/gh/przemek83/data-explorer-csharp/graph/badge.svg?token=4AZLB03ZL3)](https://codecov.io/gh/przemek83/data-explorer-csharp)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=przemek83_data-explorer-csharp&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=przemek83_data-explorer-csharp)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=przemek83_data-explorer-csharp&metric=bugs)](https://sonarcloud.io/summary/new_code?id=przemek83_data-explorer-csharp)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=przemek83_data-explorer-csharp&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=przemek83_data-explorer-csharp)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=przemek83_data-explorer-csharp&metric=coverage)](https://sonarcloud.io/summary/new_code?id=przemek83_data-explorer-csharp)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=przemek83_data-explorer-csharp&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=przemek83_data-explorer-csharp)

# Table of content
- [About Project](#about-project)
- [Problem description](#problem-description)
- [Building](#building)
- [Usage ](#usage)
- [Input data format](#input-data-format)
- [Testing](#testing)
- [License](#license)

Work in progress

# About Project
A small tool for aggregating and grouping data. Written in C#, it mimics the functionality of my older data-explorer project, which was written in C++. Created to refresh C# knowledge during the recruitment process and actual work. Moreover, allowing to exercise TDD and have some fun.

# Problem description
For given input data, allow calculating the average, minimum and maximum, taking into consideration the grouping column.

# Building
First, you need to download the repo to your machine. Make sure you have .Net installed, and the version is greater or equal to `8.0`. Having that done, use the following command to build:
```
dotnet build
```
When executed in the root directory of the repository, it should create a binary called `DataExplorer` deep in `src` folder hierarchy. 

# Usage
Having file `file` and `DataExplorer` binary in the current directory, the application can be launched via command:
`DataExplorer file {avg,min,max} aggregation grouping`  
Where:  
+ `file` - name of file with data to load,  
+ `{avg,min,max}` - type of operation; use one of those,  
+ `aggregation` - name of column used for aggregating data,  
+ `grouping` - name of column used for grouping data.

Example usage:  
`DataExplorer sample.txt avg score first_name`  

Example output:
```
Execute: AVG score GROUP BY first_name
tim: 8
tamas: 5
dave: 8
```

# Input data format
TODO

# Testing
TODO

# License
The project is distributed under the MIT License. See `LICENSE` for more information.