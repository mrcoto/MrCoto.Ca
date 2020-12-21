using FluentMigrator;

namespace MrCoto.Ca.Infrastructure.Migrations
{
    [Migration(20201116101600)]
    public class AddInitialTables : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql("CREATE EXTENSION \"uuid-ossp\"");
            
            Create.Table("g_tenant").WithDescription("Organizaciones")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("code").AsString(40).NotNullable().WithColumnDescription("Código de Organización")
                .WithColumn("name").AsString(200).NotNullable().WithColumnDescription("Nombre de la Organización")
                .WithColumn("deleted_at").AsDateTime().Nullable().WithColumnDescription("Flag de Borrado Lógico")
                .WithColumn("created_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Creación")
                .WithColumn("updated_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Actualización")
                ;
            
            Create.Table("g_role").WithDescription("Roles")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("code").AsString(40).NotNullable().WithColumnDescription("Código del Rol")
                .WithColumn("name").AsString(200).NotNullable().WithColumnDescription("Nombre del Rol")
                .WithColumn("deleted_at").AsDateTime().Nullable().WithColumnDescription("Flag de Borrado Lógico")
                ;
            
            Create.Table("g_user").WithDescription("Cuentas de Usuario")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("name").AsString(200).NotNullable().WithColumnDescription("Nombre")
                .WithColumn("email").AsString(80).NotNullable().WithColumnDescription("Correo Electrónico")
                .WithColumn("password").AsString(255).NotNullable().WithColumnDescription("Contraseña")
                .WithColumn("g_tenant_id").AsInt64().ForeignKey("g_tenant", "id").NotNullable().Indexed().WithColumnDescription("Organización Asociada")
                .WithColumn("g_role_id").AsInt64().ForeignKey("g_role", "id").NotNullable().Indexed().WithColumnDescription("Rol Asociado")
                .WithColumn("login_attempts").AsInt32().NotNullable().WithDefaultValue(0).WithColumnDescription("Intentos de Inicio de Sesión")
                .WithColumn("disabled_account_at").AsDateTime().Nullable().WithColumnDescription("Fecha/Hora se deshabilitación de la cuenta")
                .WithColumn("last_login_at").AsDateTime().Nullable().WithColumnDescription("Última Fecha/Hora de Inicio de Sesión")
                .WithColumn("deleted_at").AsDateTime().Nullable().WithColumnDescription("Flag de Borrado Lógico")
                .WithColumn("created_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Creación")
                .WithColumn("updated_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Actualización")
                ;
            
            Create.Table("g_disablement_type").WithDescription("Tipo de Deshabilitación/Habilitación")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("description").AsString(120).NotNullable().WithColumnDescription("Descripción")
                .WithColumn("deleted_at").AsDateTime().Nullable().WithColumnDescription("Flag de Borrado Lógico")
                .WithColumn("created_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Creación")
                .WithColumn("updated_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Actualización")
                ;
            
            Create.Table("g_password_reset").WithDescription("Reestablecimiento de Contraseña")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("email").AsString(80).NotNullable().Indexed().WithColumnDescription("Email")
                .WithColumn("token").AsString(255).NotNullable().WithColumnDescription("Token de reestablecimiento")
                .WithColumn("created_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Creación")
                .WithColumn("updated_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Actualización")
                ;
            
            Create.Table("g_user_disablement").WithDescription("Histórico de deshabilitaciones/habilitaciones")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("g_user_id").AsInt64().ForeignKey("g_user", "id").NotNullable().Indexed().WithColumnDescription("Usuario Habilitado/Deshabilitado")
                .WithColumn("g_disablement_type_id").AsInt64().ForeignKey("g_disablement_type", "id").NotNullable().Indexed().WithColumnDescription("Tipo de Habilitación/Deshabilitación")
                .WithColumn("observation").AsString(255).WithDefaultValue("").NotNullable().WithColumnDescription("Observaciones")
                .WithColumn("g_auth_user_id").AsInt64().ForeignKey("g_user", "id").NotNullable().Indexed().WithColumnDescription("Usuario responsable de la Habilitación/Deshabilitación")
                .WithColumn("deleted_at").AsDateTime().Nullable().WithColumnDescription("Flag de Borrado Lógico")
                .WithColumn("created_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Creación")
                .WithColumn("updated_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Actualización")
                ;
            
            Create.Table("g_login_max_attempt").WithDescription("Intentos máximos de acceso")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("max_attempts").AsInt32().NotNullable().WithDefaultValue(3).WithColumnDescription("Intentos máximos de login")
                .WithColumn("created_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Creación")
                .WithColumn("updated_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Actualización")
                ;
            
            Create.Table("g_user_login").WithDescription("Histórico de Inicios de Sesión de un Usuario")
                .WithColumn("id").AsInt64().PrimaryKey().Identity().WithColumnDescription("Identificador")
                .WithColumn("g_user_id").AsInt64().ForeignKey("g_user", "id").NotNullable().Indexed().WithColumnDescription("Usuario que inicia sesión")
                .WithColumn("device").AsString().WithDefaultValue("").WithColumnDescription("Dispositivo")
                .WithColumn("device_type").AsString().WithDefaultValue("").WithColumnDescription("Tipo de Dispositivo")
                .WithColumn("device_family").AsString().WithDefaultValue("").WithColumnDescription("Familia del Dispositivo")
                .WithColumn("browser").AsString().WithDefaultValue("").WithColumnDescription("Navegador")
                .WithColumn("browser_family").AsString().WithDefaultValue("").WithColumnDescription("Familia del Navegador")
                .WithColumn("user_agent").AsString(500).WithDefaultValue("").WithColumnDescription("User Agent")
                .WithColumn("client_ip").AsCustom("INET").WithDefaultValue("0.0.0.0").NotNullable().WithColumnDescription("IP del Cliente")
                .WithColumn("latitude").AsDecimal(18, 7).WithDefaultValue(0).NotNullable().WithColumnDescription("Latitud")
                .WithColumn("longitude").AsDecimal(18, 7).WithDefaultValue(0).NotNullable().WithColumnDescription("Longitud")
                .WithColumn("country").AsString().WithDefaultValue("").WithColumnDescription("País")
                .WithColumn("region").AsString().WithDefaultValue("").WithColumnDescription("Región")
                .WithColumn("city").AsString().WithDefaultValue("").WithColumnDescription("Ciudad")
                .WithColumn("deleted_at").AsDateTime().Nullable().WithColumnDescription("Flag de Borrado Lógico")
                .WithColumn("created_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Creación")
                .WithColumn("updated_at").AsDateTime().NotNullable().WithColumnDescription("Fecha/Hora Actualización")
                ;

            Create.Index("idx_g_user_login_id_created")
                .OnTable("g_user_login")
                .OnColumn("g_user_id")
                .Ascending()
                .OnColumn("created_at")
                .Descending();

            Execute.Sql("CREATE EXTENSION IF NOT EXISTS UNACCENT");
            
        }
    }
}