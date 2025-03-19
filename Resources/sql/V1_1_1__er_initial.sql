--  TABLA TIPOLOGÍAS (1. crm_typologies)
CREATE TABLE IF NOT EXISTS crm_typologies
(
    typology_id         BIGSERIAL PRIMARY KEY,
    parent_typology_id  BIGINT,  

    description         TEXT NOT NULL DEFAULT 'S/D',
    value1              TEXT NOT NULL DEFAULT 'S/D',
    value2              TEXT NOT NULL DEFAULT 'S/D',
    value3              TEXT NOT NULL DEFAULT 'S/D',
    state               BIGINT NOT NULL DEFAULT 501,  -- 501 = Activo

    created_by          BIGINT NOT NULL DEFAULT 0,
    creation_date       TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by         BIGINT NOT NULL DEFAULT 0,
    modification_date   TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Clave foránea autorreferenciada
    CONSTRAINT fk_parent_typology FOREIGN KEY (parent_typology_id)
    REFERENCES crm_typologies (typology_id) ON DELETE SET NULL  -- Si borras el padre, los hijos quedan huérfanos con parent_typology_id = NULL
    );

COMMENT ON TABLE crm_typologies IS 'TABLA DE TIPOLOGÍAS GENERALES DEL CRM';
COMMENT ON COLUMN crm_typologies.typology_id IS 'IDENTIFICADOR UNICO DE LA TIPOLOGIA';
COMMENT ON COLUMN crm_typologies.parent_typology_id IS 'REFERENCIA A LA TIPOLOGÍA PADRE (NULL si es raíz)';
COMMENT ON COLUMN crm_typologies.description IS 'DESCRIPCIÓN DE LA TIPOLOGÍA';
COMMENT ON COLUMN crm_typologies.value1 IS 'VALOR ADICIONAL 1';
COMMENT ON COLUMN crm_typologies.value2 IS 'VALOR ADICIONAL 2';
COMMENT ON COLUMN crm_typologies.value3 IS 'VALOR ADICIONAL 3';
COMMENT ON COLUMN crm_typologies.state IS 'ESTADO DE LA TIPOLOGÍA (501 = ACTIVO)';
COMMENT ON COLUMN crm_typologies.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_typologies.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_typologies.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_typologies.modification_date IS 'AUDITORIA: FECHA MODIFICACION';

        
--  TABLA PERSONAS (2. crm_persons)---------------------------------------------
CREATE TABLE IF NOT EXISTS crm_persons
(
    person_id              BIGSERIAL PRIMARY KEY,
    person_key             VARCHAR(255) NOT NULL UNIQUE,  -- Clave única de la persona

    first_name             VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Primer nombre
    second_name            VARCHAR(255) DEFAULT NULL,  -- Segundo nombre
    first_surname          VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Primer apellido
    second_surname         VARCHAR(255) DEFAULT NULL,  -- Segundo apellido

    birthdate              DATE DEFAULT NULL,  -- Fecha de nacimiento
    gender                 BIGINT NOT NULL DEFAULT 290
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 290 = "Gender"

    blood_type             BIGINT NOT NULL DEFAULT 300
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 300 = "Blood Type"

    profession             TEXT DEFAULT NULL,  -- Profesión

    cui                    BIGINT UNIQUE DEFAULT NULL,  -- Documento de identidad único
    nit                    VARCHAR(50) UNIQUE DEFAULT NULL,  -- Número de Identificación Tributaria

    email                  VARCHAR(255) UNIQUE DEFAULT NULL,  -- Correo electrónico
    phone_number           VARCHAR(50) DEFAULT NULL,  -- Número de teléfono principal
    secondary_phone_number VARCHAR(50) DEFAULT NULL,  -- Número de teléfono secundario
    address                VARCHAR(500) DEFAULT NULL,  -- Dirección de la persona

    state                  BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 501 = "Activo"

-- Auditoría
    created_by             BIGINT NOT NULL DEFAULT 0,
    creation_date          TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by            BIGINT NOT NULL DEFAULT 0,
    modification_date      TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
    );

-- COMENTARIOS

COMMENT ON TABLE crm_persons IS 'TABLA DE PERSONAS REGISTRADAS EN EL CRM';
COMMENT ON COLUMN crm_persons.person_id IS 'IDENTIFICADOR UNICO DE LA PERSONA';
COMMENT ON COLUMN crm_persons.person_key IS 'CLAVE UNICA DE LA PERSONA';
COMMENT ON COLUMN crm_persons.first_name IS 'PRIMER NOMBRE';
COMMENT ON COLUMN crm_persons.second_name IS 'SEGUNDO NOMBRE (OPCIONAL)';
COMMENT ON COLUMN crm_persons.first_surname IS 'PRIMER APELLIDO';
COMMENT ON COLUMN crm_persons.second_surname IS 'SEGUNDO APELLIDO (OPCIONAL)';
COMMENT ON COLUMN crm_persons.birthdate IS 'FECHA DE NACIMIENTO';
COMMENT ON COLUMN crm_persons.gender IS 'GENERO (FK A crm_typologies, 290 = GENDER)';
COMMENT ON COLUMN crm_persons.blood_type IS 'TIPO DE SANGRE (FK A crm_typologies, 300 = BLOOD TYPE)';
COMMENT ON COLUMN crm_persons.profession IS 'PROFESION';
COMMENT ON COLUMN crm_persons.cui IS 'CODIGO UNICO DE IDENTIFICACION';
COMMENT ON COLUMN crm_persons.nit IS 'NUMERO DE IDENTIFICACION TRIBUTARIA';
COMMENT ON COLUMN crm_persons.email IS 'CORREO ELECTRONICO UNICO';
COMMENT ON COLUMN crm_persons.phone_number IS 'NUMERO DE TELEFONO PRINCIPAL';
COMMENT ON COLUMN crm_persons.secondary_phone_number IS 'NUMERO DE TELEFONO SECUNDARIO';
COMMENT ON COLUMN crm_persons.address IS 'DIRECCION DE LA PERSONA';
COMMENT ON COLUMN crm_persons.state IS 'ESTADO DEL REGISTRO (FK A crm_typologies, 501 = ACTIVO)';

COMMENT ON COLUMN crm_persons.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_persons.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_persons.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_persons.modification_date IS 'AUDITORIA: FECHA MODIFICACION'; 
        
--  TABLA USUARIOS (3. crm_users)-----------------------------------------------------------
CREATE TABLE IF NOT EXISTS crm_users
(
    user_id               BIGSERIAL PRIMARY KEY,
    user_key              VARCHAR(255) UNIQUE NOT NULL,  -- Clave única del usuario
    parent_user_id        BIGINT DEFAULT NULL,  -- Usuario superior (si aplica)
    person_id             BIGINT NOT NULL,  -- Relación con crm_persons

    user_name             VARCHAR(255) UNIQUE NOT NULL,  -- Nombre de usuario único
    password              TEXT NOT NULL,  -- Contraseña encriptada
    password_change_date  TIMESTAMP WITH TIME ZONE DEFAULT NOW(),  -- Última fecha de cambio de contraseña
    access_attempt        INTEGER NOT NULL DEFAULT 0,  -- Intentos de acceso fallidos

    full_name             VARCHAR(500) NOT NULL DEFAULT 'S/D',  -- Nombre completo del usuario
    user_email            VARCHAR(255) UNIQUE NOT NULL,  -- Correo único
    user_phone            VARCHAR(50) NOT NULL DEFAULT 'S/D',  -- Teléfono del usuario

    professional_number   VARCHAR(100) DEFAULT NULL,  -- Número de colegiado (si aplica)
    signature             TEXT DEFAULT NULL,  -- Firma digital (si aplica)
    image_url             TEXT DEFAULT NULL,  -- Foto o avatar del usuario

    contact_status        BIGINT NOT NULL DEFAULT 211
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 211 = "Nuevo"

    state                 BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 501 = "Activo"

-- Auditoría
    created_by            BIGINT NOT NULL DEFAULT 0,
    creation_date         TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by           BIGINT NOT NULL DEFAULT 0,
    modification_date     TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_person FOREIGN KEY (person_id) REFERENCES crm_persons (person_id) ON DELETE CASCADE,
    CONSTRAINT fk_parent_user FOREIGN KEY (parent_user_id) REFERENCES crm_users (user_id) ON DELETE SET NULL
    );

-- COMENTARIOS

COMMENT ON TABLE crm_users IS 'TABLA DE USUARIOS DEL CRM';

COMMENT ON COLUMN crm_users.user_id IS 'IDENTIFICADOR UNICO DEL USUARIO';
COMMENT ON COLUMN crm_users.user_key IS 'CLAVE UNICA DEL USUARIO';
COMMENT ON COLUMN crm_users.parent_user_id IS 'ID DEL USUARIO SUPERIOR (SI APLICA)';
COMMENT ON COLUMN crm_users.person_id IS 'REFERENCIA A crm_persons';

COMMENT ON COLUMN crm_users.user_name IS 'NOMBRE DE USUARIO UNICO';
COMMENT ON COLUMN crm_users.password IS 'CONTRASEÑA ENCRIPTADA';
COMMENT ON COLUMN crm_users.password_change_date IS 'ULTIMA FECHA DE CAMBIO DE CONTRASEÑA';
COMMENT ON COLUMN crm_users.access_attempt IS 'INTENTOS FALLIDOS DE ACCESO';

COMMENT ON COLUMN crm_users.full_name IS 'NOMBRE COMPLETO DEL USUARIO';
COMMENT ON COLUMN crm_users.user_email IS 'CORREO ELECTRONICO UNICO';
COMMENT ON COLUMN crm_users.user_phone IS 'NUMERO DE TELEFONO DEL USUARIO';
COMMENT ON COLUMN crm_users.professional_number IS 'NUMERO DE COLEGIADO (SI APLICA)';
COMMENT ON COLUMN crm_users.signature IS 'FIRMA DIGITAL DEL USUARIO';
COMMENT ON COLUMN crm_users.image_url IS 'URL DE IMAGEN DEL USUARIO';

COMMENT ON COLUMN crm_users.contact_status IS 'ESTADO DEL CONTACTO (FK A crm_typologies, 211 = NUEVO)';
COMMENT ON COLUMN crm_users.state IS 'ESTADO DEL USUARIO (FK A crm_typologies, 501 = ACTIVO)';

COMMENT ON COLUMN crm_users.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_users.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_users.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_users.modification_date IS 'AUDITORIA: FECHA MODIFICACION';        
                
        
-- TABLA ORGANIZACIONES (4. crm_organizations)---------------------
CREATE TABLE IF NOT EXISTS crm_organizations
(
    organization_id         BIGSERIAL PRIMARY KEY,
    organization_name       VARCHAR(255) UNIQUE NOT NULL DEFAULT 'S/D',  -- Nombre único de la organización
    organization_phone      VARCHAR(50) NOT NULL DEFAULT 'S/D', -- Teléfono
    logo_url               TEXT DEFAULT NULL, -- URL del logo de la organización
    primary_contact_email   VARCHAR(255) UNIQUE DEFAULT NULL, -- Email de contacto único

    sector_type            BIGINT NOT NULL DEFAULT 220
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT, -- 220 = "Sector Type"

-- Auditoría
    created_by             BIGINT NOT NULL DEFAULT 0,
    creation_date          TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by            BIGINT NOT NULL DEFAULT 0,
    modification_date      TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
    );

-- COMENTARIOS

COMMENT ON TABLE crm_organizations IS 'TABLA DE ORGANIZACIONES CLIENTES';

COMMENT ON COLUMN crm_organizations.organization_id IS 'IDENTIFICADOR UNICO DE LA ORGANIZACION';
COMMENT ON COLUMN crm_organizations.organization_name IS 'NOMBRE UNICO DE LA ORGANIZACION';
COMMENT ON COLUMN crm_organizations.organization_phone IS 'NUMERO DE TELEFONO DE LA ORGANIZACION';
COMMENT ON COLUMN crm_organizations.logo_url IS 'URL DEL LOGO DE LA ORGANIZACION';
COMMENT ON COLUMN crm_organizations.primary_contact_email IS 'CORREO ELECTRONICO DE CONTACTO PRINCIPAL';
COMMENT ON COLUMN crm_organizations.sector_type IS 'TIPO DE SECTOR DE LA ORGANIZACION (FK A crm_typologies, 220 = SECTOR TYPE)';

COMMENT ON COLUMN crm_organizations.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_organizations.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_organizations.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_organizations.modification_date IS 'AUDITORIA: FECHA MODIFICACION';
        
--  TABLA CLIENTES (5. crm_customers)-------------------------------------------------------------
CREATE TABLE IF NOT EXISTS crm_customers
(
    customer_id            BIGSERIAL PRIMARY KEY,
    organization_id        BIGINT NOT NULL,  -- Referencia a crm_organizations

    customer_full_name     VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Nombre completo del cliente
    customer_job_position  VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Cargo en la organización
    customer_email         VARCHAR(255) UNIQUE DEFAULT NULL,  -- Correo electrónico único
    customer_primary_phone VARCHAR(50) NOT NULL DEFAULT 'S/D',  -- Teléfono principal
    customer_secondary_phone VARCHAR(50) DEFAULT NULL,  -- Teléfono secundario
    customer_nit           VARCHAR(50) UNIQUE DEFAULT NULL,  -- Número de identificación tributaria
    customer_secondary_email VARCHAR(255) UNIQUE DEFAULT NULL,  -- Correo secundario

    contact_source         BIGINT NOT NULL DEFAULT 200
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 200 = "Contact Source"

    contact_status         BIGINT NOT NULL DEFAULT 211
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 211 = "Nuevo"

-- Auditoría
    created_by            BIGINT NOT NULL DEFAULT 0,
    creation_date         TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by           BIGINT NOT NULL DEFAULT 0,
    modification_date     TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_organization FOREIGN KEY (organization_id) REFERENCES crm_organizations (organization_id) ON DELETE CASCADE
    );

-- COMENTARIOS

COMMENT ON TABLE crm_customers IS 'TABLA DE CLIENTES';

COMMENT ON COLUMN crm_customers.customer_id IS 'IDENTIFICADOR UNICO DEL CLIENTE';
COMMENT ON COLUMN crm_customers.organization_id IS 'REFERENCIA A crm_organizations';
COMMENT ON COLUMN crm_customers.customer_full_name IS 'NOMBRE COMPLETO DEL CLIENTE';
COMMENT ON COLUMN crm_customers.customer_job_position IS 'CARGO DEL CLIENTE EN LA ORGANIZACION';
COMMENT ON COLUMN crm_customers.customer_email IS 'CORREO ELECTRONICO UNICO DEL CLIENTE';
COMMENT ON COLUMN crm_customers.customer_primary_phone IS 'NUMERO DE TELEFONO PRINCIPAL';
COMMENT ON COLUMN crm_customers.customer_secondary_phone IS 'NUMERO DE TELEFONO SECUNDARIO';
COMMENT ON COLUMN crm_customers.customer_nit IS 'NUMERO DE IDENTIFICACION TRIBUTARIA';
COMMENT ON COLUMN crm_customers.customer_secondary_email IS 'CORREO ELECTRONICO SECUNDARIO';
COMMENT ON COLUMN crm_customers.contact_source IS 'FUENTE DE CONTACTO (FK A crm_typologies, 200 = CONTACT SOURCE)';
COMMENT ON COLUMN crm_customers.contact_status IS 'ESTADO DEL CONTACTO (FK A crm_typologies, 211 = NUEVO)';

COMMENT ON COLUMN crm_customers.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_customers.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_customers.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_customers.modification_date IS 'AUDITORIA: FECHA MODIFICACION';   
        
--  TABLA PRODUCTOS (6. crm_products)---------------------------------------------------------
CREATE TABLE IF NOT EXISTS crm_products
(
    product_id         BIGSERIAL PRIMARY KEY,
    product_name       VARCHAR(255) NOT NULL UNIQUE,
    product_description TEXT DEFAULT 'S/D',

    product_category   BIGINT NOT NULL DEFAULT 260
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 260 = "Product Category"

    stock             INTEGER NOT NULL DEFAULT 0,  -- Cantidad disponible en inventario
    price             NUMERIC(18,2) NOT NULL DEFAULT 0.00,  -- Precio del producto
    currency          BIGINT NOT NULL DEFAULT 250
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 250 = "Currency"

    status            BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 501 = "Activo"

-- Auditoría
    created_by        BIGINT NOT NULL DEFAULT 0,
    creation_date     TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by       BIGINT NOT NULL DEFAULT 0,
    modification_date TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
    );

-- COMENTARIOS

COMMENT ON TABLE crm_products IS 'TABLA DE PRODUCTOS DISPONIBLES EN EL CRM';

COMMENT ON COLUMN crm_products.product_id IS 'IDENTIFICADOR UNICO DEL PRODUCTO';
COMMENT ON COLUMN crm_products.product_name IS 'NOMBRE UNICO DEL PRODUCTO';
COMMENT ON COLUMN crm_products.product_description IS 'DESCRIPCION DEL PRODUCTO';
COMMENT ON COLUMN crm_products.product_category IS 'CATEGORIA DEL PRODUCTO (FK A crm_typologies, 260 = PRODUCT CATEGORY)';
COMMENT ON COLUMN crm_products.stock IS 'CANTIDAD DISPONIBLE EN INVENTARIO';
COMMENT ON COLUMN crm_products.price IS 'PRECIO DEL PRODUCTO';
COMMENT ON COLUMN crm_products.currency IS 'MONEDA DEL PRECIO (FK A crm_typologies, 250 = CURRENCY)';
COMMENT ON COLUMN crm_products.status IS 'ESTADO DEL PRODUCTO (FK A crm_typologies, 501 = ACTIVO)';

COMMENT ON COLUMN crm_products.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_products.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_products.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_products.modification_date IS 'AUDITORIA: FECHA MODIFICACION';
        
--  TABLA TICKETS (7. crm_tickets)-----------------------------------------------------------
CREATE TABLE IF NOT EXISTS crm_tickets
(
    ticket_id        BIGSERIAL PRIMARY KEY,
    ticket_key       VARCHAR(255) NOT NULL UNIQUE,  -- Clave única del ticket

    customer_id      BIGINT NOT NULL,  -- Referencia a crm_customers
    user_id         BIGINT DEFAULT NULL,  -- Referencia a crm_users

    subject         VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Asunto del ticket
    description     TEXT DEFAULT 'S/D',  -- Descripción del problema o solicitud

    status          BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 501 = "Activo"

    priority        BIGINT NOT NULL DEFAULT 320
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 320 = "Alta"

    category        BIGINT NOT NULL DEFAULT 330
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 330 = "Ticket Category"

    open_date      TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),  -- Fecha de apertura del ticket
    close_date     TIMESTAMP WITH TIME ZONE DEFAULT NULL,  -- Fecha de cierre del ticket

-- Auditoría
    created_by     BIGINT NOT NULL DEFAULT 0,
    creation_date  TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by    BIGINT NOT NULL DEFAULT 0,
    modification_date TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_customer FOREIGN KEY (customer_id) REFERENCES crm_customers (customer_id) ON DELETE CASCADE,
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES crm_users (user_id) ON DELETE SET NULL
    );

-- COMENTARIOS

COMMENT ON TABLE crm_tickets IS 'TABLA DE TICKETS DE SOPORTE O SOLICITUDES';

COMMENT ON COLUMN crm_tickets.ticket_id IS 'IDENTIFICADOR UNICO DEL TICKET';
COMMENT ON COLUMN crm_tickets.ticket_key IS 'CLAVE UNICA DEL TICKET';
COMMENT ON COLUMN crm_tickets.customer_id IS 'REFERENCIA A crm_customers';
COMMENT ON COLUMN crm_tickets.user_id IS 'REFERENCIA A crm_users';
COMMENT ON COLUMN crm_tickets.subject IS 'ASUNTO DEL TICKET';
COMMENT ON COLUMN crm_tickets.description IS 'DESCRIPCION DEL PROBLEMA O SOLICITUD';
COMMENT ON COLUMN crm_tickets.status IS 'ESTADO DEL TICKET (FK A crm_typologies, 501 = ACTIVO)';
COMMENT ON COLUMN crm_tickets.priority IS 'PRIORIDAD DEL TICKET (FK A crm_typologies, 320 = ALTA)';
COMMENT ON COLUMN crm_tickets.category IS 'CATEGORIA DEL TICKET (FK A crm_typologies, 330 = TICKET CATEGORY)';
COMMENT ON COLUMN crm_tickets.open_date IS 'FECHA DE APERTURA DEL TICKET';
COMMENT ON COLUMN crm_tickets.close_date IS 'FECHA DE CIERRE DEL TICKET';

COMMENT ON COLUMN crm_tickets.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_tickets.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_tickets.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_tickets.modification_date IS 'AUDITORIA: FECHA MODIFICACION';           
        

        
        

--  TABLA EQUIPOS (8. crm_teams)----------------------------------------------------
CREATE TABLE IF NOT EXISTS crm_teams
(
    team_id          BIGSERIAL PRIMARY KEY,
    team_name        VARCHAR(255) NOT NULL UNIQUE,  -- Nombre único del equipo
    team_description TEXT DEFAULT 'S/D',  -- Descripción del equipo

    state            BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- Estado del equipo (501 = "Activo")

-- Auditoría
    created_by       BIGINT NOT NULL DEFAULT 0,
    creation_date    TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by      BIGINT NOT NULL DEFAULT 0,
    modification_date TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
    );

-- COMENTARIOS

COMMENT ON TABLE crm_teams IS 'TABLA DE EQUIPOS DE USUARIOS';

COMMENT ON COLUMN crm_teams.team_id IS 'IDENTIFICADOR UNICO DEL EQUIPO';
COMMENT ON COLUMN crm_teams.team_name IS 'NOMBRE UNICO DEL EQUIPO';
COMMENT ON COLUMN crm_teams.team_description IS 'DESCRIPCION DEL EQUIPO';
COMMENT ON COLUMN crm_teams.state IS 'ESTADO DEL EQUIPO (FK A crm_typologies, 501 = ACTIVO)';

COMMENT ON COLUMN crm_teams.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_teams.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_teams.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_teams.modification_date IS 'AUDITORIA: FECHA MODIFICACION';  
        
 --  TABLA RELACIÓN USUARIOS - EQUIPOS (9. crm_users_teams)--------------------------------------
CREATE TABLE IF NOT EXISTS crm_users_teams
(
    users_team_id   BIGSERIAL PRIMARY KEY,
    user_id         BIGINT NOT NULL,  -- Referencia a crm_users
    team_id         BIGINT NOT NULL,  -- Referencia a crm_teams

    role            BIGINT NOT NULL DEFAULT 310
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 310 = "Role"

    state           BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 501 = "Activo"

-- Auditoría
    created_by      BIGINT NOT NULL DEFAULT 0,
    creation_date   TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by     BIGINT NOT NULL DEFAULT 0,
    modification_date TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES crm_users (user_id) ON DELETE CASCADE,
    CONSTRAINT fk_team FOREIGN KEY (team_id) REFERENCES crm_teams (team_id) ON DELETE CASCADE
    );

-- COMENTARIOS

COMMENT ON TABLE crm_users_teams IS 'TABLA DE RELACIÓN ENTRE USUARIOS Y EQUIPOS';

COMMENT ON COLUMN crm_users_teams.users_team_id IS 'IDENTIFICADOR UNICO DE LA RELACION USUARIO - EQUIPO';
COMMENT ON COLUMN crm_users_teams.user_id IS 'REFERENCIA A crm_users';
COMMENT ON COLUMN crm_users_teams.team_id IS 'REFERENCIA A crm_teams';
COMMENT ON COLUMN crm_users_teams.role IS 'ROL DEL USUARIO EN EL EQUIPO (FK A crm_typologies, 310 = ROLE)';
COMMENT ON COLUMN crm_users_teams.state IS 'ESTADO DE LA RELACION (FK A crm_typologies, 501 = ACTIVO)';

COMMENT ON COLUMN crm_users_teams.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_users_teams.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_users_teams.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_users_teams.modification_date IS 'AUDITORIA: FECHA MODIFICACION';  
        
        
--  TABLA ESTABLECIMIENTOS (10. crm_establishment)------------------------------
CREATE TABLE IF NOT EXISTS crm_establishment
(
    establishment_id         BIGSERIAL PRIMARY KEY,
    parent_establishment_id  BIGINT DEFAULT NULL,  -- Referencia a otro establecimiento (si aplica)
    establishment_key        VARCHAR(255) UNIQUE NOT NULL,  -- Clave única del establecimiento

    establishment_name       VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Nombre del establecimiento
    establishment_description TEXT DEFAULT 'S/D',  -- Descripción
    establishment_address    VARCHAR(500) NOT NULL DEFAULT 'S/D', -- Dirección
    establishment_email      VARCHAR(255) UNIQUE NOT NULL, -- Email único
    establishment_phone      VARCHAR(50) NOT NULL DEFAULT 'S/D', -- Teléfono
    establishment_type       BIGINT NOT NULL DEFAULT 220
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT, -- 220 = "Sector Type"

    establishment_status     BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT, -- 501 = "Activo"

-- Auditoría
    created_by              BIGINT NOT NULL DEFAULT 0,
    creation_date           TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by             BIGINT NOT NULL DEFAULT 0,
    modification_date       TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_parent_establishment FOREIGN KEY (parent_establishment_id)
    REFERENCES crm_establishment (establishment_id) ON DELETE SET NULL
    );

-- COMENTARIOS

COMMENT ON TABLE crm_establishment IS 'TABLA DE ESTABLECIMIENTOS O SUCURSALES';

COMMENT ON COLUMN crm_establishment.establishment_id IS 'IDENTIFICADOR UNICO DEL ESTABLECIMIENTO';
COMMENT ON COLUMN crm_establishment.parent_establishment_id IS 'ID DEL ESTABLECIMIENTO PADRE (SI APLICA)';
COMMENT ON COLUMN crm_establishment.establishment_key IS 'CLAVE UNICA DEL ESTABLECIMIENTO';
COMMENT ON COLUMN crm_establishment.establishment_name IS 'NOMBRE DEL ESTABLECIMIENTO';
COMMENT ON COLUMN crm_establishment.establishment_description IS 'DESCRIPCION DEL ESTABLECIMIENTO';
COMMENT ON COLUMN crm_establishment.establishment_address IS 'DIRECCION DEL ESTABLECIMIENTO';
COMMENT ON COLUMN crm_establishment.establishment_email IS 'CORREO ELECTRONICO UNICO DEL ESTABLECIMIENTO';
COMMENT ON COLUMN crm_establishment.establishment_phone IS 'NUMERO DE TELEFONO DEL ESTABLECIMIENTO';
COMMENT ON COLUMN crm_establishment.establishment_type IS 'TIPO DE ESTABLECIMIENTO (FK A crm_typologies, 220 = SECTOR TYPE)';
COMMENT ON COLUMN crm_establishment.establishment_status IS 'ESTADO DEL ESTABLECIMIENTO (FK A crm_typologies, 501 = ACTIVO)';

COMMENT ON COLUMN crm_establishment.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_establishment.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_establishment.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_establishment.modification_date IS 'AUDITORIA: FECHA MODIFICACION';
        
--  TABLA RELACIÓN USUARIOS - ESTABLECIMIENTOS (11. crm_users_establishment)----------------------
CREATE TABLE IF NOT EXISTS crm_users_establishment
(
    users_sucursal_id  BIGSERIAL PRIMARY KEY,
    user_id            BIGINT NOT NULL,  -- Referencia a crm_users
    establishment_id   BIGINT NOT NULL,  -- Referencia a crm_establishment

    role               BIGINT NOT NULL DEFAULT 310
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 310 = "Role"

    state              BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 501 = "Activo"

-- Auditoría
    created_by         BIGINT NOT NULL DEFAULT 0,
    creation_date      TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by        BIGINT NOT NULL DEFAULT 0,
    modification_date  TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES crm_users (user_id) ON DELETE CASCADE,
    CONSTRAINT fk_establishment FOREIGN KEY (establishment_id) REFERENCES crm_establishment (establishment_id) ON DELETE CASCADE
    );

-- COMENTARIOS

COMMENT ON TABLE crm_users_establishment IS 'TABLA DE RELACIÓN ENTRE USUARIOS Y ESTABLECIMIENTOS';

COMMENT ON COLUMN crm_users_establishment.users_sucursal_id IS 'IDENTIFICADOR UNICO DE LA RELACION USUARIO - ESTABLECIMIENTO';
COMMENT ON COLUMN crm_users_establishment.user_id IS 'REFERENCIA A crm_users';
COMMENT ON COLUMN crm_users_establishment.establishment_id IS 'REFERENCIA A crm_establishment';
COMMENT ON COLUMN crm_users_establishment.role IS 'ROL DEL USUARIO EN EL ESTABLECIMIENTO (FK A crm_typologies, 310 = ROLE)';
COMMENT ON COLUMN crm_users_establishment.state IS 'ESTADO DE LA RELACION (FK A crm_typologies, 501 = ACTIVO)';

COMMENT ON COLUMN crm_users_establishment.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_users_establishment.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_users_establishment.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_users_establishment.modification_date IS 'AUDITORIA: FECHA MODIFICACION';
        


-- TABLA LEADS (12. crm_leads)----------------------------------------------------
CREATE TABLE IF NOT EXISTS crm_leads
(
    lead_id               BIGSERIAL PRIMARY KEY,
    customer_id           BIGINT NOT NULL,  -- Referencia a crm_customers
    user_id               BIGINT DEFAULT NULL,  -- Referencia a crm_users

    lead_name             VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Nombre del lead
    lead_description      TEXT DEFAULT 'S/D',  -- Descripción del lead

    lead_pipeline_stage   BIGINT NOT NULL DEFAULT 230
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 230 = "Opportunity Stage"

    lead_status           BIGINT NOT NULL DEFAULT 211
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 211 = "Nuevo"

    priority              BIGINT NOT NULL DEFAULT 320
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 320 = "Alta"

    next_action_date      TIMESTAMP WITH TIME ZONE DEFAULT NULL, -- Próxima acción

    lead_source           BIGINT NOT NULL DEFAULT 200
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 200 = "Contact Source"

    lead_type             VARCHAR(255) NOT NULL DEFAULT 'S/D', -- Tipo de lead

-- Auditoría
    created_by            BIGINT NOT NULL DEFAULT 0,
    creation_date         TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by           BIGINT NOT NULL DEFAULT 0,
    modification_date     TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_customer FOREIGN KEY (customer_id) REFERENCES crm_customers (customer_id) ON DELETE CASCADE,
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES crm_users (user_id) ON DELETE SET NULL
    );

-- COMENTARIOS

COMMENT ON TABLE crm_leads IS 'TABLA DE LEADS DE CLIENTES';

COMMENT ON COLUMN crm_leads.lead_id IS 'IDENTIFICADOR UNICO DEL LEAD';
COMMENT ON COLUMN crm_leads.customer_id IS 'REFERENCIA A crm_customers';
COMMENT ON COLUMN crm_leads.user_id IS 'REFERENCIA A crm_users';
COMMENT ON COLUMN crm_leads.lead_name IS 'NOMBRE DEL LEAD';
COMMENT ON COLUMN crm_leads.lead_description IS 'DESCRIPCION DEL LEAD';
COMMENT ON COLUMN crm_leads.lead_pipeline_stage IS 'ETAPA DEL LEAD EN EL PIPELINE (FK A crm_typologies, 230 = OPPORTUNITY STAGE)';
COMMENT ON COLUMN crm_leads.lead_status IS 'ESTADO DEL LEAD (FK A crm_typologies, 211 = NUEVO)';
COMMENT ON COLUMN crm_leads.priority IS 'PRIORIDAD DEL LEAD (FK A crm_typologies, 320 = ALTA)';
COMMENT ON COLUMN crm_leads.next_action_date IS 'FECHA DE PROXIMA ACCION';
COMMENT ON COLUMN crm_leads.lead_source IS 'FUENTE DEL LEAD (FK A crm_typologies, 200 = CONTACT SOURCE)';
COMMENT ON COLUMN crm_leads.lead_type IS 'TIPO DE LEAD';

COMMENT ON COLUMN crm_leads.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_leads.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_leads.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_leads.modification_date IS 'AUDITORIA: FECHA MODIFICACION';
        
-- TABLA OPORTUNIDADES (13. crm_opportunities)-------------------------------------------
CREATE TABLE IF NOT EXISTS crm_opportunities
(
    opportunity_id        BIGSERIAL PRIMARY KEY,
    lead_id              BIGINT NOT NULL,  -- Referencia a crm_leads
    user_id              BIGINT DEFAULT NULL,  -- Referencia a crm_users
    organization_id      BIGINT NOT NULL,  -- Referencia a crm_organizations

    opportunity_name     VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Nombre de la oportunidad
    opportunity_description TEXT DEFAULT 'S/D',  -- Descripción

    status              BIGINT NOT NULL DEFAULT 240
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 240 = "Deal Status"

    probability         INTEGER NOT NULL DEFAULT 0,  -- Probabilidad de éxito en porcentaje
    estimated_value     NUMERIC(18,2) NOT NULL DEFAULT 0.00,  -- Valor estimado de la oportunidad

    currency            BIGINT NOT NULL DEFAULT 250
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 250 = "Currency"

    close_date         TIMESTAMP WITH TIME ZONE DEFAULT NULL,  -- Fecha estimada de cierre
    product_id         BIGINT DEFAULT NULL,  -- Producto asociado (si aplica)

    stage              BIGINT NOT NULL DEFAULT 230
        
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 230 = "Opportunity Stage"

-- Auditoría
    created_by         BIGINT NOT NULL DEFAULT 0,
    creation_date      TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by        BIGINT NOT NULL DEFAULT 0,
    modification_date  TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_lead FOREIGN KEY (lead_id) REFERENCES crm_leads (lead_id) ON DELETE CASCADE,
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES crm_users (user_id)  ON DELETE SET NULL,
    CONSTRAINT fk_organization FOREIGN KEY (organization_id) REFERENCES crm_organizations (organization_id) ON DELETE CASCADE,
    CONSTRAINT fk_product FOREIGN KEY (product_id) REFERENCES crm_products (product_id) ON DELETE SET NULL
    );

-- COMENTARIOS

COMMENT ON TABLE crm_opportunities IS 'TABLA DE OPORTUNIDADES DE NEGOCIO';

COMMENT ON COLUMN crm_opportunities.opportunity_id IS 'IDENTIFICADOR UNICO DE LA OPORTUNIDAD';
COMMENT ON COLUMN crm_opportunities.lead_id IS 'REFERENCIA A crm_leads';
COMMENT ON COLUMN crm_opportunities.user_id IS 'REFERENCIA A crm_users';
COMMENT ON COLUMN crm_opportunities.organization_id IS 'REFERENCIA A crm_organizations';
COMMENT ON COLUMN crm_opportunities.opportunity_name IS 'NOMBRE DE LA OPORTUNIDAD';
COMMENT ON COLUMN crm_opportunities.opportunity_description IS 'DESCRIPCION DE LA OPORTUNIDAD';
COMMENT ON COLUMN crm_opportunities.status IS 'ESTADO DE LA OPORTUNIDAD (FK A crm_typologies, 240 = DEAL STATUS)';
COMMENT ON COLUMN crm_opportunities.probability IS 'PROBABILIDAD DE EXITO EN PORCENTAJE';
COMMENT ON COLUMN crm_opportunities.estimated_value IS 'VALOR ESTIMADO DE LA OPORTUNIDAD';
COMMENT ON COLUMN crm_opportunities.currency IS 'MONEDA DE LA OPORTUNIDAD (FK A crm_typologies, 250 = CURRENCY)';
COMMENT ON COLUMN crm_opportunities.close_date IS 'FECHA ESTIMADA DE CIERRE';
COMMENT ON COLUMN crm_opportunities.product_id IS 'REFERENCIA A crm_products (SI APLICA)';
COMMENT ON COLUMN crm_opportunities.stage IS 'ETAPA DE LA OPORTUNIDAD (FK A crm_typologies, 230 = OPPORTUNITY STAGE)';

COMMENT ON COLUMN crm_opportunities.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_opportunities.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_opportunities.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_opportunities.modification_date IS 'AUDITORIA: FECHA MODIFICACION'; 
        
--  TABLA NEGOCIOS/CERRADOS (14. crm_deals)---------------------------------------------
CREATE TABLE IF NOT EXISTS crm_deals
(
    deal_id              BIGSERIAL PRIMARY KEY,
    customer_id          BIGINT NOT NULL,  -- Referencia a crm_customers
    opportunity_id       BIGINT NOT NULL,  -- Referencia a crm_opportunities
    organization_id      BIGINT NOT NULL,  -- Referencia a crm_organizations
    user_id              BIGINT DEFAULT NULL,  -- Referencia a crm_users

    deal_name           VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Nombre del negocio
    deal_description    TEXT DEFAULT 'S/D',  -- Descripción del negocio

    deal_status         BIGINT NOT NULL DEFAULT 240
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 240 = "Deal Status"

    deal_value          NUMERIC(18,2) NOT NULL DEFAULT 0.00,  -- Valor final del negocio
    currency            BIGINT NOT NULL DEFAULT 250
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 250 = "Currency"

    close_date         TIMESTAMP WITH TIME ZONE DEFAULT NOW(),  -- Fecha de cierre

-- Auditoría
    created_by         BIGINT NOT NULL DEFAULT 0,
    creation_date      TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by        BIGINT NOT NULL DEFAULT 0,
    modification_date  TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_customer FOREIGN KEY (customer_id) REFERENCES crm_customers (customer_id) ON DELETE CASCADE,
    CONSTRAINT fk_opportunity FOREIGN KEY (opportunity_id) REFERENCES crm_opportunities (opportunity_id) ON DELETE CASCADE,
    CONSTRAINT fk_organization FOREIGN KEY (organization_id) REFERENCES crm_organizations (organization_id) ON DELETE CASCADE,
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES crm_users (user_id) ON DELETE SET NULL
    );

-- COMENTARIOS

COMMENT ON TABLE crm_deals IS 'TABLA DE NEGOCIOS CERRADOS';

COMMENT ON COLUMN crm_deals.deal_id IS 'IDENTIFICADOR UNICO DEL NEGOCIO';
COMMENT ON COLUMN crm_deals.customer_id IS 'REFERENCIA A crm_customers';
COMMENT ON COLUMN crm_deals.opportunity_id IS 'REFERENCIA A crm_opportunities';
COMMENT ON COLUMN crm_deals.organization_id IS 'REFERENCIA A crm_organizations';
COMMENT ON COLUMN crm_deals.user_id IS 'REFERENCIA A crm_users';
COMMENT ON COLUMN crm_deals.deal_name IS 'NOMBRE DEL NEGOCIO';
COMMENT ON COLUMN crm_deals.deal_description IS 'DESCRIPCION DEL NEGOCIO';
COMMENT ON COLUMN crm_deals.deal_status IS 'ESTADO DEL NEGOCIO (FK A crm_typologies, 240 = DEAL STATUS)';
COMMENT ON COLUMN crm_deals.deal_value IS 'VALOR FINAL DEL NEGOCIO';
COMMENT ON COLUMN crm_deals.currency IS 'MONEDA DEL NEGOCIO (FK A crm_typologies, 250 = CURRENCY)';
COMMENT ON COLUMN crm_deals.close_date IS 'FECHA DE CIERRE DEL NEGOCIO';

COMMENT ON COLUMN crm_deals.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_deals.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_deals.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_deals.modification_date IS 'AUDITORIA: FECHA MODIFICACION';    
        
--  TABLA ACTIVIDADES (15. crm_activities)--------------------------------------------------
CREATE TABLE IF NOT EXISTS crm_activities
(
    activity_id        BIGSERIAL PRIMARY KEY,
    user_id           BIGINT DEFAULT NULL,  -- Referencia a crm_users
    customer_id       BIGINT DEFAULT NULL,  -- Referencia a crm_customers
    lead_id           BIGINT DEFAULT NULL,  -- Referencia a crm_leads
    organization_id   BIGINT DEFAULT NULL,  -- Referencia a crm_organizations
    opportunity_id    BIGINT DEFAULT NULL,  -- Referencia a crm_opportunities
    deal_id          BIGINT DEFAULT NULL,  -- Referencia a crm_deals
    ticket_id        BIGINT DEFAULT NULL,  -- Referencia a crm_tickets

    activity_type    BIGINT NOT NULL DEFAULT 270
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 270 = "Activity Type"

    subject          VARCHAR(255) NOT NULL DEFAULT 'S/D',  -- Asunto de la actividad
    description      TEXT DEFAULT 'S/D',  -- Descripción de la actividad

    status          BIGINT NOT NULL DEFAULT 501
    REFERENCES crm_typologies (typology_id) ON DELETE SET DEFAULT,  -- 501 = "Activo"

    scheduled_date  TIMESTAMP WITH TIME ZONE DEFAULT NULL,  -- Fecha programada
    completed_date  TIMESTAMP WITH TIME ZONE DEFAULT NULL,  -- Fecha de finalización

-- Auditoría
    created_by      BIGINT NOT NULL DEFAULT 0,
    creation_date   TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    modified_by     BIGINT NOT NULL DEFAULT 0,
    modification_date TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),

    -- Relaciones
    CONSTRAINT fk_user FOREIGN KEY (user_id)
    REFERENCES crm_users (user_id)
                                            ON DELETE SET NULL,
    CONSTRAINT fk_customer FOREIGN KEY (customer_id) REFERENCES crm_customers (customer_id) ON DELETE SET NULL,
    CONSTRAINT fk_lead FOREIGN KEY (lead_id) REFERENCES crm_leads (lead_id) ON DELETE SET NULL,
    CONSTRAINT fk_organization FOREIGN KEY (organization_id) REFERENCES crm_organizations (organization_id) ON DELETE SET NULL,
    CONSTRAINT fk_opportunity FOREIGN KEY (opportunity_id) REFERENCES crm_opportunities (opportunity_id) ON DELETE SET NULL,
    CONSTRAINT fk_deal FOREIGN KEY (deal_id) REFERENCES crm_deals (deal_id) ON DELETE SET NULL,
    CONSTRAINT fk_ticket FOREIGN KEY (ticket_id) REFERENCES crm_tickets (ticket_id) ON DELETE SET NULL
    );

-- COMENTARIOS

COMMENT ON TABLE crm_activities IS 'TABLA DE ACTIVIDADES REGISTRADAS EN EL CRM';

COMMENT ON COLUMN crm_activities.activity_id IS 'IDENTIFICADOR UNICO DE LA ACTIVIDAD';
COMMENT ON COLUMN crm_activities.user_id IS 'REFERENCIA A crm_users';
COMMENT ON COLUMN crm_activities.customer_id IS 'REFERENCIA A crm_customers (SI APLICA)';
COMMENT ON COLUMN crm_activities.lead_id IS 'REFERENCIA A crm_leads (SI APLICA)';
COMMENT ON COLUMN crm_activities.organization_id IS 'REFERENCIA A crm_organizations (SI APLICA)';
COMMENT ON COLUMN crm_activities.opportunity_id IS 'REFERENCIA A crm_opportunities (SI APLICA)';
COMMENT ON COLUMN crm_activities.deal_id IS 'REFERENCIA A crm_deals (SI APLICA)';
COMMENT ON COLUMN crm_activities.ticket_id IS 'REFERENCIA A crm_tickets (SI APLICA)';
COMMENT ON COLUMN crm_activities.activity_type IS 'TIPO DE ACTIVIDAD (FK A crm_typologies, 270 = ACTIVITY TYPE)';
COMMENT ON COLUMN crm_activities.subject IS 'ASUNTO DE LA ACTIVIDAD';
COMMENT ON COLUMN crm_activities.description IS 'DESCRIPCION DE LA ACTIVIDAD';
COMMENT ON COLUMN crm_activities.status IS 'ESTADO DE LA ACTIVIDAD (FK A crm_typologies, 501 = ACTIVO)';
COMMENT ON COLUMN crm_activities.scheduled_date IS 'FECHA PROGRAMADA PARA LA ACTIVIDAD';
COMMENT ON COLUMN crm_activities.completed_date IS 'FECHA EN QUE SE COMPLETO LA ACTIVIDAD';

COMMENT ON COLUMN crm_activities.created_by IS 'AUDITORIA: CREADO POR';
COMMENT ON COLUMN crm_activities.creation_date IS 'AUDITORIA: FECHA CREACION';
COMMENT ON COLUMN crm_activities.modified_by IS 'AUDITORIA: MODIFICADO POR';
COMMENT ON COLUMN crm_activities.modification_date IS 'AUDITORIA: FECHA MODIFICACION';        
        
        

          