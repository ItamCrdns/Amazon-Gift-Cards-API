## Amazon Gift Cards API

## Descripción
Este proyecto es un backend desarrollado con .NET 8 diseñado para interactuar con el sandbox de la API de Amazon Incentives. Provee endpoints para
* Crear tarjetas de regalo
* Revisar su estado de activación
* Cancelar una tarjeta de regalo
* Activar una tarjeta de regalo que no haya sido previamente preactivada
* Desactivar la tarjeta de regalo

## Construido con

* .NET 8
* AWSSDK.CORE
* RestSharp (para hacer las solicitues al API de Amazon Incentives)
* xUnit, FakeItEasy, FluentAssertions, Coverlet (para pruebas unitarias)

## Uso
### Requisitos previos
- [.NET SDK](https://dotnet.microsoft.com/download) (versión 8.0)

### Instalación
- Restaurar paquetes NuGet
  ```
  dotnet restore
  ```
- Compilar y correr el proyecto
  ```
  dotnet build
  dotnet run
  ```

### Pruebas
- Correr las pruebas usando
  ```
  dotnet test --collect:"XPlat Code Coverage"
  ```
- Generar un reporte detallado
  ```
  reportgenerator -reports:"./Tests/TestResults/**/*.cobertura.xml" -targetdir:./CoverageReport -reporttypes:Html
  ```
Puedes consultar el reporte detallado de los tests en Tests\CoverageReport\index.html

## Variables de entorno
Para correr este proyecto, debes agregar las siguientes variables de entorno al appsettings.json en el proyecto y en los tests

```
  "Amazon": {
    "AWSAccessKey": "ACCESS_KEY",
    "AWSSecret": "SECRET",
    "PartnerId": "PARTNERID",
    "CurrenyCode": "CURRENCYCODE",
    "Region": "eu-west-1",
    "AGCODUrl": "https://agcod-v2-eu-gamma.amazon.com"
  },
```
