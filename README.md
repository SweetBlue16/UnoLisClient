# 🎮 UNO-LIS Client
Bienvenido al cliente oficial de **UNO-LIS**, una versión digital multijugador basada en el clásico juego de cartas UNO. Cliente de escritorio del juego UNO-LIS, desarrollado en WPF y C# 7.3 con conexión al servidor mediante WCF.

---

## 🧩 Descripción general
El cliente UNO-LIS proporciona una interfaz amigable desarrollada en Windows Presentation Foundation (WPF), permitiendo al jugador conectarse al servidor, gestionar su perfil y participar en partidas en línea. Este repositorio contiene todo el código fuente de la aplicación cliente (el juego en sí) que se conecta a `UnoLisServer` para una experiencia multijugador en tiempo real.

## ⚙️ Tecnologías principales
| Componente | Tecnología |
| -------------- | -------------- |
| Lenguaje | C# 7.3 |
| Framework | .NET Framework 4.7.2 |
| Interfaz | Windows Presentation Foundation (WPF) |
| Comunicación | Windows Communication Foundation (WCF) |
| ORM | Entity Framework 6.5.1 |
| Logging | log4net 3.2.0 |
| Análisis | SonarQube |

## 🧠 Funcionalidades principales
- Inicio de sesión, registro y validación de usuarios.
- Cambio dinámico de idioma (inglés/español).
- Lista de amigos, chat y solicitudes.
- Partidas multijugador con sincronización en tiempo real.
- Tienda con compra de ítems cosméticos.
- Configuración de perfil, sonido y fondos.

## 🚀 Ejecución local
1. Clona el repositorio:
```bash
git clone https://github.com/SweetBlue16/UnoLisClient.git
```
2. Abre la solución en **Visual Studio 2022.**
3. Configura **UnoLisClient.UI** como proyecto de inicio.
4. Ajusta la URL del servidor WCF en `App.config` si es necesario.
5. Ejecuta y juega.

## 👥 Autores
- Mauricio
- Erickmel

## 🏫 Licencia
Proyecto académico para la experiencia educativa de **Tecnologías para la Construcción de Software** en la Universidad Veracruzana, Facultad de Estadística e Informática. Uso educativo sin fines comerciales.
