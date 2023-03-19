# Projekt i Webbutveckling med .NET (DT191G)
I detta repository finns källkodsfiler för en databasdriven applikation skapad med ASP.NET Core MVC och Entity Framework Core. Webbplatsen är en portfolio och består av en publik del där data läses ut från databasen och en administrativ del där det även finns möjlighet att lägga till, ändra och radera data. För inloggning används den inbyggda funktionaliteten Identity. På webbplatsen har jag också använt mig av CSS-ramverket Bootstrap.

## Modeller
- Category: Används för att hantera data för kategorier
- Competence: Används för att hantera data för kompetenser
- Experience: Används för att hantera data för utbildningar och jobb
- Image: Används för att hantera uppladdning och data för bilder
- Project: Använd för att hantera data för projekt

### Vymodeller
- ProjectViewModel: Används för att hantera data för både projekt och kompetenser
- CompInProject: Används för att hantera data som krävs för att använda checkboxar i formulären för projekt 

## Kontroller
- CategoryController: innehåller actions för CRUD-funktionalitet för kategorier
- CompetenceController: innehåller actions med CRUD-funktionalitet för kompetenser
- ExperienceController: innehåller actions med CRUD-funktionalitet för utbildningar och jobb
- HomeController: innehåller actions med READ-funktionalitet för kompetenser, utbildningar och jobb samt projekt.
- ImageController: innehåller actions med funktionalitet för uppladdning av bild samt CRUD-funktionalitet för information om bilder
- ProjectController: innehåller actions med CRUD-funktionalitet för projekt

## Vyer
Vyer renderas ut med hjälp av filen _Layout.

- Category
  - Create
  - Edit
  - Delete
  - Index
- Competence
  - Create
  - Edit
  - Delete
  - Index
- Experience
  - Create
  - Edit
  - Delete
  - Details
  - Index
- Home
  - About
  - Admin
  - Contact
  - CV
  - Index
  - Project
  - Projects
- Image
  - Create
  - Edit
  - Delete
  - Details
  - Index
- Project
  - Create
  - Edit
  - Delete
  - Details
  - Index
  
## Om repositoriet
Skapat av Sofia Widholm 2022

Webbutveckling med .NET, Webbutvecklingsprogrammet, Mittuniversitetet
