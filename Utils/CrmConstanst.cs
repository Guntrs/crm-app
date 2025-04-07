namespace crm_core.Utils
{
    public class CrmConstants
    {
        public const long TIPOLOGIA_GLOBAL = 100;

        // Fuentes de Contacto
        public const long FUENTE_CONTACTO = 200;
        public const long REDES_SOCIALES = 201;
        public const long REFERIDO = 202;
        public const long CORREO = 203;
        public const long TELEFONO = 204;
        public const long FORMULARIO = 205;

        // Estados de Contacto
        public const long ESTADO_CONTACTO = 210;
        public const long NUEVO_CONTACTO = 211;
        public const long CONTACTADO = 212;
        public const long CONTACTO_INACTIVO = 213;
        public const long VIGENTE = 214;

        // Sectores
        public const long SECTOR = 220;
        public const long SECTOR_TECNOLOGIA = 221;
        public const long SECTOR_COMERCIO = 222;
        public const long SECTOR_SERVICIOS = 223;
        public const long SECTOR_ADMINISTRACION = 224;
        public const long SECTOR_PUBLICO = 225;
        public const long SECTOR_SALUD = 226;

        // Etapas de Oportunidad
        public const long ETAPA_OPORTUNIDAD = 230;
        public const long ETAPA_ABIERTA = 231;
        public const long ETAPA_CERRADA = 232;
        public const long ETAPA_CALIFICADA = 233;
        public const long ETAPA_NEGOCIACION = 234;
        
        // Estados de Negociación
        public const long ESTADO_NEGOCIACION = 240;
        public const long ESTADO_NEGOCIACION_ACTIVA = 241;
        public const long ESTADO_NEGOCIACION_CANCELADA = 242;
        public const long ESTADO_NEGOCIACION_INACTIVA = 243;

        // Monedas
        public const long MONEDA = 250;
        public const long MONEDA_USD = 251;
        public const long MONEDA_GTQ = 252;

        // Categorías de Producto
        public const long CATEGORIA_PRODUCTO = 260;
        public const long CATEGORIA_SOFTWARE_A_MEDIDA = 261;
        public const long CATEGORIA_CONSULTORIA = 262;
        public const long CATEGORIA_SAAS = 263;
        public const long CATEGORIA_INTEGRACIONES = 264;
        public const long CATEGORIA_SOPORTE = 265;

        // Tipos de Actividad
        public const long ACTIVIDAD = 270;
        public const long ACTIVIDAD_LLAMADA = 271;
        public const long ACTIVIDAD_REUNION = 272;
        public const long ACTIVIDAD_SEGUIMIENTO = 273;
        public const long ACTIVIDAD_PENDIENTE = 274;
        public const long ACTIVIDAD_COMPLETADA = 275;

        // Género
        public const long GENERO = 290;
        public const long GENERO_MASCULINO = 291;
        public const long GENERO_FEMENINO = 292;

        // Tipo de Sangre
        public const long TIPO_SANGRE = 300;
        public const long TIPO_SANGRE_O_POS = 301;
        public const long TIPO_SANGRE_O_NEG = 302;
        public const long TIPO_SANGRE_A_POS = 303;
        public const long TIPO_SANGRE_B_POS = 304;

        // Roles
        public const long ROL = 310;
        public const long ROL_ADMINISTRADOR = 311;
        public const long ROL_OPERADOR = 312;
        public const long ROL_USUARIO = 313;
        public const long ROL_DOCTOR = 314;

        // Prioridad de Ticket
        public const long PRIORIDAD_TICKET = 320;
        public const long PRIORIDAD_ALTA = 321;
        public const long PRIORIDAD_MEDIA = 322;
        public const long PRIORIDAD_BAJA = 323;

        // Categoría de Ticket
        public const long CATEGORIA_TICKET = 330;
        public const long SOPORTE = 331;
        public const long FACTURACION = 332;
        public const long LEGAL = 333;
        public const long PRODUCCION = 334;
        public const long FINANZAS = 335;

        // Tipo de Estado
        public const long TIPO_ESTADO = 500;
        public const long ESTADO_ACTIVO = 501;
        public const long ESTADO_INACTIVO = 502;
        public const long ESTADO_SUSPENDIDO = 503;

        // Cargo en la Empresa
        public const long CARGO_EMPRESA = 510;
        public const long CARGO_GERENTE = 511;
        public const long CARGO_DIRECTOR = 512;
        public const long CARGO_VENTAS = 513;
        public const long CARGO_ANALISTA = 514;
        public const long CARGO_CEO = 515;

        // Tipos de Profesión
        public const long PROFESION = 520;
        public const long PROFESION_COMERCIANTE = 521;
        public const long PROFESION_ESTUDIANTE = 522;
        public const long PROFESION_GRADUADO = 523;
        public const long PROFESION_EMPLEADO = 524;
        public const long PROFESION_TECNICO = 525;
        public const long PROFESION_ADMINISTRADOR = 526;
        public const long PROFESION_EMPRESARIO = 527;
        public const long PROFESION_DOCTOR = 528;

        // Tipo de Cliente
        public const long TIPO_CLIENTE = 530;
        public const long CLIENTE_NUEVO = 531;
        public const long CLIENTE_RECURRENTE = 532;
        public const long CLIENTE_INTERESADO_SOPORTE = 533;
        public const long CLIENTE_OPORTUNIDAD_NEGOCIO = 534;
        public const long CLIENTE_COLABORADOR_PARTNER = 535;
    }
}