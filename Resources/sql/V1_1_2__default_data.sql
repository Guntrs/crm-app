INSERT INTO crm_typologies
(
    typology_id,
    parent_typology_id,
    description,
    value1,
    value2,
    value3,
    state,
    created_by,
    creation_date,
    modified_by,
    modification_date
)
VALUES
    (100, NULL, 'Global', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (200, 100, 'Fuentes de Contacto', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (201, 200, 'Redes Sociales', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (202, 200, 'Referido', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (203, 200, 'Correo', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (204, 200, 'Telefono', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (205, 200, 'Formulario', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (210, 100, 'Estados de Contacto', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (211, 210, 'Nuevo Contacto', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (212, 210, 'Contactado', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (213, 210, 'Contacto Inactivo', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (214, 210, 'Vigente', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (220, 100, 'Sectores', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (221, 220, 'Tecnologia', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (222, 220, 'Comercio', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (223, 220, 'Servicios', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (224, 220, 'Administracion', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (225, 220, 'Publico', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (226, 220, 'Salud', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (230, 100, 'Etapas de Oportunidad', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (231, 230, 'Abierta', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (232, 230, 'Cerrada', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (233, 230, 'Calificada', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (234, 230, 'Negociacion', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (240, 100, 'Estados de Negociacion', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (241, 240, 'Negociación Activa', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (242, 240, 'Cancelada', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (243, 240, 'Negociación Inactiva', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (250, 100, 'Monedas', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (251, 250, 'USD', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (252, 250, 'GTQ', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (260, 100, 'Categorias de Producto', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (261, 260, 'Desarrollo de Software y Soluciones a Medida', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (262, 260, 'Servicios Profesionales y Consultoría', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (263, 260, 'Software como Servicio', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (264, 260, 'Integraciones y APIs', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (265, 260, 'Soporte, Mantenimiento y SLA', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (270, 100, 'Tipos de Actividad', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (271, 270, 'Llamada', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (272, 270, 'Reunion', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (273, 270, 'Seguimiento', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (274, 270, 'Pendiente', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (275, 270, 'Completada', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (290, 100, 'Genero', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (291, 290, 'Masculino', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (292, 290, 'Femenino', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (300, 100, 'Tipo de Sangre', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (301, 300, 'O+', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (302, 300, 'O-', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (303, 300, 'A+', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (304, 300, 'B+', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (310, 100, 'Roles', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (311, 310, 'Administrador', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (312, 310, 'Operador', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (313, 310, 'Usuario', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (314, 310, 'Doctor', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (320, 100, 'Prioridad de Ticket', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (321, 320, 'Alta', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (322, 320, 'Media', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (323, 320, 'Baja', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (330, 100, 'Categoria de Ticket', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (331, 330, 'Soporte', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (332, 330, 'Facturacion', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (333, 330, 'Legal', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (334, 330, 'Produccion', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (335, 330, 'Finanzas', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (500, 100, 'Tipo de Estado', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (501, 500, 'Activo', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (502, 500, 'Inactivo', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (503, 500, 'Suspendido', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (510, 100, 'Cargo en la Empresa', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (511, 510, 'Gerente', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (512, 510, 'Director', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (513, 510, 'Ejecutivo de Ventas', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (514, 510, 'Analista', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (515, 510, 'CEO', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (520, 100, 'Tipos de Profesion', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (521, 520, 'Comerciante', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (522, 520, 'Estudiante', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (523, 520, 'Graduado', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (524, 520, 'Empleado', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (525, 520, 'Tecnico', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (526, 520, 'Administrador', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (527, 520, 'Empresario', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (528, 520, 'Doctor', '%', '%', '%', 501, 1, NOW(), 0, NOW()),

    (530, 100, 'Tipo de Cliente', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (531, 530, 'Nuevo Cliente', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (532, 530, 'Recurrente', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (533, 530, 'Interesando en Soporte', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (534, 530, 'Oportunidad de Negocio', '%', '%', '%', 501, 1, NOW(), 0, NOW()),
    (535, 530, 'Colaborador_Partner', '%', '%', '%', 501, 1, NOW(), 0, NOW());


