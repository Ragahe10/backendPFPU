# backendPFPU
##🛠️Tecnologías utilizadas

###⚙️ ASP.NET Core 9  
    Es un framework open-source y multiplataforma para construir aplicaciones web modernas, APIs RESTful y microservicios de alto rendimiento  
**Instalación:**  
    dotnet new webapi --use-controllers  
**Uso básico:**  
    ASP.NET Core te permite definir controladores (Controllers) y rutas ([HttpGet], [HttpPost], etc.) para exponer tu lógica en forma de endpoints accesibles por HTTP.

### 🗃️ SQLite  
    Es un motor de base de datos ligero y autónomo que guarda los datos en un solo archivo. Es ideal para aplicaciones pequeñas y medianas donde no se requiere un servidor de base de datos completo.  
**Instalación:**  
    dotnet add package Microsoft.Data.Sqlite --version 9.0.0  
**Uso básico:**  
    Puedes conectar tu API a SQLite usando Entity Framework Core o directamente desde código con SqliteConnection. Se suele usar para persistencia de usuarios, productos, sesiones, etc.
  
### 🔐 JWT (Json Web Token)  
    Es un estándar para autenticación basada en tokens. Permite a los usuarios autenticarse sin mantener sesiones en el servidor. Cada token contiene la información del usuario y es firmado digitalmente.  
**Instalación:**  
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer  
**Uso básico:**  
    - Generar un token al iniciar sesión.  
    - Incluirlo en el encabezado Authorization: Bearer <token> al consumir la API.  
    - Proteger endpoints con [Authorize].  

### 🔑 BCrypt  
    Es una función de hash para proteger contraseñas. Es resistente a ataques por fuerza bruta y se usa comúnmente para cifrar contraseñas antes de guardarlas en la base de datos.  
**Instalación:**  
    dotnet add package BCrypt.Net-Next  
**Uso básico:**  
    - Cuando un usuario se registra:  
        - Tomas la contraseña que escribió (por ejemplo, "hola123").  
        - Usas BCrypt.HashPassword("hola123") para generar una versión cifrada (como "1a8df...").  
        - Guardas esa versión cifrada en la base de datos, no la contraseña original.  
    - Cuando un usuario inicia sesión:  
        - Tomas la contraseña que el usuario ingresó.  
        - Recuperas la versión cifrada de la base de datos.  
        - Usas BCrypt.Verify(contraseñaIngresada, contraseñaCifrada) para verificar si coinciden.  
        - Si coincide, la contraseña es correcta y puedes permitir el acceso.  

### 📧 MailKit  
    Es una librería .NET para enviar correos electrónicos utilizando protocolos como SMTP. Es útil para enviar confirmaciones, recuperación de contraseñas, notificaciones, etc.  
**Instalación:**  
    dotnet add package MailKit  
**Uso básico:**  
    - Crear el mensaje:  
        - Defines quién lo envía (From) y quién lo recibe (To).  
        - Escribes un asunto (Subject) y el contenido del mensaje (Body).  
    - Conectar con el servidor SMTP (el que realmente envía el correo):  
        - Usas un servidor como Gmail, Outlook, SendGrid, etc.  
        - Proporcionas tus credenciales (email y contraseña o token de aplicación).  
        - Te conectas al servidor con MailKit usando el protocolo SMTP.  
    - Enviar el mensaje:  
        - Una vez conectado, le dices al servidor "envía este mensaje".  
        - Cuando termina, cierras la conexión.  
