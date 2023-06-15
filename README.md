# ThunderBank
*Por Rafael Maestre del Río y Alejandro Gómez Blanco*


- [Descripción del proyecto](#descripcion)
- [Información sobre despliegue](#despliegue)
- [¿Cómo usarlo?](#instrucciones)


## Instrucciones para el desarrollo del Módulo Proyecto Integrado

### <a id="descripcion" /> Descripción del Proyecto: ThunderBank 🌩️🏦

ThunderBank es una aplicación bancaria que proporciona a los usuarios una experiencia segura y conveniente para gestionar sus cuentas bancarias. Con una interfaz intuitiva y funciones avanzadas, ThunderBank ofrece una amplia gama de características tanto para los usuarios como para los responsables del banco.

La aplicación se centra en brindar a los usuarios un control completo sobre sus cuentas bancarias, permitiéndoles realizar operaciones como visualizar saldos y gestionar tarjetas. Además, ThunderBank también ofrece funciones especiales para los responsables del banco, como la administración de clientes.

Características principales:

- **Autenticación segura**: Los usuarios pueden acceder a sus cuentas mediante autenticación de usuario, utilizando credenciales seguras como nombre de usuario y contraseña. Se implementan medidas de seguridad para proteger la información de los usuarios.

- **Visualización de cuentas**: Los usuarios pueden ver información detallada sobre sus cuentas bancarias.

- **Transferencias bancarias**: Los usuarios pueden realizar transferencias entre sus propias cuentas.

- **Creación de tarjetas**: Los usuarios tienen la posibilidad de crear nuevas tarjetas para utilizar en sus cuentas bancarias.

- **Gestión de clientes (para responsables)**: Los responsables del banco tienen acceso a un listado completo de todos los clientes, independientemente de si están a su cargo o no. Esto les permite tener una visión general y asignarse todos aquellos que están libres.

ThunderBank busca brindar una experiencia bancaria completa y eficiente, asegurando la privacidad y la seguridad de los usuarios. Con una interfaz fácil de usar y funciones avanzadas. 💼💰

### <a id="despliegue" /> Información sobre el despliegue 🚀 
El proyecto ha sido desplegado en un hosting gratuito, alojando la base de datos en el mismo.
Para mas seguridad de la integridad de los datos, la cadena de conexión a la base de datos ha sido almacenada en los secrets del proyecto.

Se puede acceder a la aplicación en el siguiente enlace:
>http://kretinoh1-001-site1.itempurl.com/


La opción de despliegue en local que tienes es la siguiente: 

1. Clonar el repositorio: Debes clonar el repositorio de GitHub en tu máquina local utilizando un cliente de Git o descargando el código fuente como un archivo ZIP.


2. Instalar .NET SDK: Debes mirar si tienes instalado el SDK de .NET Core 6 en su máquina. Puede descargar e instalar el SDK desde el sitio web oficial de .NET Core (https://dotnet.microsoft.com/download/dotnet).


3. Restaurar las dependencias: Abra una línea de comandos (terminal) en el directorio raíz del proyecto clonado y ejecute el comando `` dotnet restore ``. Esto descargará todas las dependencias necesarias para compilar y ejecutar el proyecto


4. Compilar el proyecto: En la misma línea de comandos, ejecute el comando `` dotnet build `` para compilar el proyecto. Esto generará los archivos binarios necesarios para ejecutar la aplicación.


5. Ejecutar la aplicación: Una vez que la compilación se haya completado con éxito, el usuario final puede ejecutar la aplicación utilizando el comando ``dotnet run``. Esto iniciará la aplicación y estará disponible para su uso localmente en el entorno de la línea de comandos.


### <a id="instrucciones" /> ¿Cómo usarlo? 📱💼

Para utilizar esta aplicación, sigue los siguientes pasos:

1. **Autenticación de usuario** 🔐
   - Ingresa tus credenciales, como nombre de usuario y contraseña, para iniciar sesión en tu cuenta bancaria. La autenticación de usuario es una medida de seguridad esencial para proteger tu información financiera.

2. **Visualización de cuentas** 👀
   - Una vez que hayas iniciado sesión, podrás ver información detallada sobre tus cuentas bancarias. Esto incluye el saldo, las tarjetas asociadas, los movimientos realizados y otra información relevante.

3. **Movimientos** 💸
   - Utiliza la función de movimientos para enviar y recibir fondos desde y hacia tus cuentas bancarias. Puedes transferir fondos a otras cuentas dentro del mismo banco.

4. **Creación de tarjetas** 💳
   - Si deseas obtener una nueva tarjeta para tu cuenta bancaria, utiliza esta función.

Funciones adicionales para el responsable:
1. **Listado de todos los clientes** 📋
   - Obtén un listado rápido de todos los clientes del banco, independientemente de si están a tu cargo o no.

2. **Listado de tus propios clientes** 👤
   - Accede a un listado de todos los clientes que tienes a tu cargo y obtén acceso a sus datos personales.


Sigue estas instrucciones y podrás aprovechar al máximo todas las funciones de ThunderBank 🎉💰
