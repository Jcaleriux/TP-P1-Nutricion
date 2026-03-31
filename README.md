# TP-P1-Nutricion

Aplicación de escritorio desarrollada en C# con Windows Forms para registrar usuarios, productos, menús e indicadores nutricionales.

## Descripción

`Nutrición para todos` es un sistema orientado a apoyar el control de la alimentación diaria de un usuario. La aplicación permite:

- registrar e iniciar sesión con usuarios
- administrar productos alimenticios y su información nutricional
- crear, actualizar y eliminar menús diarios por tiempos de comida
- visualizar información nutricional personal
- consultar estadísticas nutricionales por día, mes y rango de fechas

El proyecto se desarrolló aplicando principios de programación orientada a objetos, MVC, separación por capas, SOLID y buenas prácticas de Clean Code.

## Tecnologías utilizadas

- C#
- .NET 10
- Windows Forms
- CSV como persistencia local
- Sonar Analyzer

## Estructura del proyecto

- `src/ClassModels`
  Contiene las entidades del sistema como `User`, `Product`, `Menu`, `MenuProduct` y los modelos de estadísticas nutricionales.

- `src/ClassController`
  Contiene la lógica de negocio, validaciones, cálculos nutricionales y manejo de archivos.

- `src/ClassViews`
  Contiene las vistas de Windows Forms y la composición principal de la aplicación.

- `data`
  Contiene los archivos CSV con usuarios, productos, menús y productos por menú.

- `Documentación`
  Contiene el entregable inicial y la documentación técnica del proyecto.

## Funcionalidades principales

### Gestión de usuarios

- registro de usuario
- inicio de sesión
- almacenamiento de datos físicos y objetivo nutricional

### Gestión de productos

- registro de productos
- edición de productos
- base inicial de 50 productos

### Gestión de menús

- creación de menús por fecha
- asociación de productos por tiempo de comida
- actualización y eliminación de menús
- cálculo automático de calorías y macronutrientes

### Información nutricional

- calorías de mantenimiento
- calorías objetivo según meta
- distribución de macronutrientes
- cálculo de IMC y categoría

### Estadísticas nutricionales

- consumo diario de calorías
- consumo diario de proteínas, carbohidratos y grasas
- comparación con meta diaria
- progreso del día
- estadísticas por rango de fechas
- conteo mensual de cumplimiento

## Datos incluidos

La aplicación incluye archivos de datos con cantidades suficientes para cumplir los mínimos del proyecto:

- más de 25 usuarios
- 50 productos
- más de 100 registros de comidas
- registros distribuidos en múltiples fechas

## Requisitos para ejecutar

- Windows
- Visual Studio 2022 o superior con soporte para .NET y Windows Forms
- .NET SDK 10

## Cómo ejecutar

1. Clonar el repositorio.
2. Abrir `ClassViews.slnx` en Visual Studio.
3. Verificar que el proyecto de inicio sea `ClassViews`.
4. Ejecutar con `F5` o `Ctrl + F5`.

También puedes compilar desde terminal con:

```powershell
dotnet build ClassViews.slnx
```

## Archivos de persistencia

La aplicación utiliza archivos CSV para cargar y guardar información:

- `data/users.csv`
- `data/products.csv`
- `data/menus.csv`
- `data/menuProducts.csv`

## Flujo de uso recomendado

1. Registrar un usuario o iniciar sesión.
2. Revisar o registrar productos en `Manage Products`.
3. Crear o modificar menús en `Manage Menus`.
4. Consultar métricas personales en `Nutrition Info`.
5. Consultar estadísticas en `Statistics`.

## Documentación

La carpeta `Documentación` contiene:

- `Entregable #1.pdf`
- `Documentacion Tecnica Final.pdf`

