# backendPFPU
Creacion de la api inicial con: 
    dotnet new webapi --use-controllers
Incluimos la interfaz de swaggers con:
    dotnet add package Swashbuckle.AspNetCore
Instalo el paquete para manejar la base de datos:
    dotnet add package Microsoft.Data.Sqlite --version 9.0.0
Instalo bcrypt para el cifrado de contraseñas
    dotnet add package BCrypt.Net-Next
