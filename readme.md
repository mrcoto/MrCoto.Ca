# CA Example API

Clean Architecture con escrito en C# y Net Core 5.0

## FAQ

1. ¿Cómo crear una Pagina `.cshtml`?

R: Ejecutando el comando:

```bash
dotnet new page --namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Mails --output Modules/GeneralModule/Users/Mails --name DisablementMail
```

Verificar que de propiedad tenga `Embedded Resource` y `Do not Copy`

2. No se ejecuta la función `Action` pasado al método de `IBackgroundJobService`

R: Verifique que:

- El método `Action` es público
- Todos los servicios se pueden resolver con inyección de dependencias
- El método `Action` se invoca como `() => Metodo()` (independiente si es asíncrono o no)
- Verifique que el `Dto` no tenga méthodos `virtual` que utilicen los **proxys de EFCORE**

3. Como ejecutar un comando en `AppCli` desde docker?

R: Primero, entre al contenedor con `docker exec -it app_ca_cli bash`
y luego, ejecute un comando como: `./MrCoto.Ca.AppCli general user list`

4. ¿Como generar el build de docker para windows?

R: Con el comando `docker-compose -f docker-compose.windows.yml build appcli appwebapi` (solo aplica para consola y webapi)

## Notas

Desarrollado por José Espinoza@2020 (espinozasalas.jose@gmail.com)