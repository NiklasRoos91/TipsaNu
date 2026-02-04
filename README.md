
# TipsaNu

TipsaNu är en applikation för sporttippning, byggd med **React + Vite** på frontend och **.NET Core** på backend.

## Funktionalitet

- Skapa och spela turneringar baserat på matcher.
- Delta i privata eller publika ligor.
- Tippa resultat och extra bets (sidospel) för varje match.
- Poängberäkning och leaderboard uppdateras automatiskt.
- Administrera matcher, resultat och turneringar.

> Notera: Applikationen hanterar ingen riktig betting utan används enbart för poäng, statistik och social jämförelse.

## Projektstruktur

```
TipsaNu/
├─ frontend/       # React + Vite frontend
├─ backend/        # .NET backend
├─ .gitignore
└─ README.md
```

## Komma igång

### Frontend

1. Navigera till frontend-mappen:

```bash
cd frontend
```

2. Installera beroenden:

```bash
npm install
```

3. Starta utvecklingsservern:

```bash
npm run dev
```

Öppna sedan webbläsaren på adressen som visas i terminalen.

### Backend

1. Navigera till backend-mappen:

```bash
cd backend
```

2. Bygg och kör projektet med Visual Studio eller via .NET CLI:

```bash
dotnet run --project TipsaNu.Api
```
## Databas & Arkitektur

TipsaNu har en relationsdatabas som hanterar:
-Användare (User) och roller
-Turneringsmallar (TournamentTemplate) och instanser (Tournament)
-Matcher (Match) och tippningar (Prediction)
-Ligor (League) och medlemskap (LeagueMember)
-Sidospel (ExtraBetOption, ExtraBet)
-Historik och loggning (PredictionHistory, ExtraBetHistory, UserActivityLog)
-Leaderboards och statistik (LeaderboardEntry, TournamentCompetitorStat)

Se mappen docs/ för UML-diagram och detaljerad databasdokumentation.

## Konfiguration

- `appsettings.json` innehåller konfigurationsinställningar för backend.
- `.gitignore` exkluderar build-filer, editor-specifika filer och temporära filer.

## Licens

Projektet är för privat/utbildningssyfte.
