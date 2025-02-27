# backendPFPU
Creacion de la api inicial con: 
    dotnet new webapi --use-controllers
Incluimos la interfaz de swaggers con:
    dotnet add package Swashbuckle.AspNetCore
Instalo el paquete para manejar la base de datos:
    dotnet add package Microsoft.Data.Sqlite --version 9.0.0
Instalo bcrypt para el cifrado de contraseñas
    dotnet add package BCrypt.Net-Next
Agrego JWT para logueo y autenticacion:
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    JSON Web Token (JWT) es un estándar para autenticación que permite a los clientes (como frontend o apps móviles) autenticarse en una API sin almacenar sesiones en el servidor. Se usa ampliamente en APIs RESTful porque es seguro, escalable y eficiente.
    Un JWT contiene tres partes codificadas en Base64:
        - Header (Encabezado): Indica el tipo de token y el algoritmo de encriptación.
        - Payload (Cuerpo): Contiene la información del usuario (claims).
        - Signature (Firma): Garantiza que el token no ha sido alterado.
    usar "[Authorize]" sobre los endpoints que necesiten autenticacion 