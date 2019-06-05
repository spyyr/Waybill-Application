# Waybill Application

<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Installation](#installation)
* [Usage](#usage)
* [Contact](#contact)



<!-- ABOUT THE PROJECT -->
## About The Project

Application was made in purpose of making part of my work much easier and smoother instead of manually copying and pasting informations from first file to the second file. Application uses 2 excel files to create waybill template in excel file. First - the source, contains a lot of informations about all placed orders, Second one is a template to which read specified data from first file is inserted. Application uses a local database with 2 tables in it. First table is named 'Computers' it contains all computer models which occurs in a source file with info of: model name, price, weight and if it has USB-C Ethernet adapter. Second table is named 'Localisations' it contains all localisations which occurs and are often used in a source file with info of: street, city, zip code.

### Built With
I have built this application using:
* [WPF](https://docs.microsoft.com/pl-pl/dotnet/framework/wpf/) - to make this application window based.
* [EPPLUS](https://github.com/JanKallman/EPPlus) - to deal with excel files with quick and easy way.


<!-- GETTING STARTED -->
## Getting Started

How to use application

### Installation

1. Download this repository.
2. Open folder ```SQL```
3. Run ```WaybillSql.sql``` using SQL Server Management Studio to create Database.
4. Install NuGet packages
```sh
Install-Package EPPlus -Version 4.5.3.1
Install-Package WindowsAPICodePack-Core -Version 1.1.2
```
5. Run ```WaybillApplication.sln``` using Visual Studio.
6. Press F5 to start debugging.

<!-- USAGE EXAMPLES -->
## Usage

Choose ```ExampleSourceFile.xlsx``` from ``` ExampleExcelFiles ``` as first file and choose ```ExampleTemplateFile``` from ```ExampleExcelFiles``` as second file, then choose saving directory and rows from first file to be inserted into second file. Last step is to click button.  Find out result file in saving directory you have chosen. If some informations were invalid in first file or database does not contain info of computer model or localisation zip code in it, then row in result file is highlighted on red-violet color. enjoy! :)

<!-- CONTACT -->
## Contact

My Email  - arek.pazola1998@gmail.com

Project Link: [https://github.com/w00lfer/Waybill-Application](https://github.com/w00lfer/Waybill-Application)

