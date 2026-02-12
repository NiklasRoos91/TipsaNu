
import { ExtraBet, TournamentTemplate, TiebreakerCriterion } from '../types/types';


export const MOCK_TEMPLATES: TournamentTemplate[] = [
  {
    id: 'tpl1',
    name: 'VM Standard (32 lag)',
    description: 'Klassiskt format med 8 grupper om 4 lag. De två bästa i varje grupp går vidare till slutspel.',
    totalGroups: 8,
    advancingPerGroup: 2,
    allowsBestThird: false,
    pointRules: [
      { id: 'r1', name: 'Standard', pointsForExactScore: 10, pointsForCorrectGoalDifference: 6, pointsForCorrectWinner: 4 }
    ],
    tiebreakers: [
      { criterion: TiebreakerCriterion.GoalDifference, priority: 1 },
      { criterion: TiebreakerCriterion.GoalsScored, priority: 2 }
    ]
  },
  {
    id: 'tpl2',
    name: 'EM Format (24 lag)',
    description: '6 grupper om 4 lag. De två bästa samt de 4 bästa treorna går vidare.',
    totalGroups: 6,
    advancingPerGroup: 2,
    allowsBestThird: true,
    pointRules: [
      { id: 'r2', name: 'Standard', pointsForExactScore: 10, pointsForCorrectGoalDifference: 6, pointsForCorrectWinner: 4 }
    ],
    tiebreakers: [
      { criterion: TiebreakerCriterion.HeadToHead, priority: 1 },
      { criterion: TiebreakerCriterion.GoalDifference, priority: 2 }
    ]
  }
];


const RAW_MATCHES_2026 = [
  { group: "A", date: "2026-06-11", time: "21:00", homeCompetitor: "Mexico", awayCompetitor: "South Africa" },
  { group: "A", date: "2026-06-12", time: "04:00", homeCompetitor: "South Korea", awayCompetitor: "TBD" },
  { group: "B", date: "2026-06-12", time: "21:00", homeCompetitor: "Canada", awayCompetitor: "TBD" },
  { group: "D", date: "2026-06-13", time: "03:00", homeCompetitor: "USA", awayCompetitor: "Paraguay" },
  { group: "B", date: "2026-06-13", time: "21:00", homeCompetitor: "Qatar", awayCompetitor: "Switzerland" },
  { group: "C", date: "2026-06-14", time: "00:00", homeCompetitor: "Brazil", awayCompetitor: "Morocco" },
  { group: "C", date: "2026-06-14", time: "03:00", homeCompetitor: "Haiti", awayCompetitor: "Scotland" },
  { group: "D", date: "2026-06-14", time: "06:00", homeCompetitor: "Australia", awayCompetitor: "TBD" },
  { group: "E", date: "2026-06-14", time: "19:00", homeCompetitor: "Germany", awayCompetitor: "Curaçao" },
  { group: "F", date: "2026-06-14", time: "22:00", homeCompetitor: "Netherlands", awayCompetitor: "Japan" },
  { group: "E", date: "2026-06-15", time: "01:00", homeCompetitor: "Ivory Coast", awayCompetitor: "Ecuador" },
  { group: "F", date: "2026-06-15", time: "04:00", homeCompetitor: "TBD", awayCompetitor: "Tunisia" },
  { group: "H", date: "2026-06-15", time: "18:00", homeCompetitor: "Spain", awayCompetitor: "Cape Verde" },
  { group: "G", date: "2026-06-15", time: "21:00", homeCompetitor: "Belgium", awayCompetitor: "Egypt" },
  { group: "H", date: "2026-06-16", time: "00:00", homeCompetitor: "Saudi Arabia", awayCompetitor: "Uruguay" },
  { group: "G", date: "2026-06-16", time: "03:00", homeCompetitor: "Iran", awayCompetitor: "New Zealand" },
  { group: "I", date: "2026-06-16", time: "21:00", homeCompetitor: "France", awayCompetitor: "Senegal" },
  { group: "I", date: "2026-06-17", time: "00:00", homeCompetitor: "TBD", awayCompetitor: "Norway" },
  { group: "J", date: "2026-06-17", time: "03:00", homeCompetitor: "Argentina", awayCompetitor: "Algeria" },
  { group: "J", date: "2026-06-17", time: "06:00", homeCompetitor: "Austria", awayCompetitor: "Jordan" },
  { group: "K", date: "2026-06-17", time: "19:00", homeCompetitor: "Portugal", awayCompetitor: "TBD" },
  { group: "L", date: "2026-06-17", time: "22:00", homeCompetitor: "England", awayCompetitor: "Croatia" },
  { group: "L", date: "2026-06-18", time: "01:00", homeCompetitor: "Ghana", awayCompetitor: "Panama" },
  { group: "K", date: "2026-06-18", time: "04:00", homeCompetitor: "Uzbekistan", awayCompetitor: "Colombia" },
  { group: "A", date: "2026-06-18", time: "18:00", homeCompetitor: "TBD", awayCompetitor: "South Africa" },
  { group: "B", date: "2026-06-18", time: "21:00", homeCompetitor: "Switzerland", awayCompetitor: "TBD" },
  { group: "B", date: "2026-06-19", time: "00:00", homeCompetitor: "Canada", awayCompetitor: "Qatar" },
  { group: "A", date: "2026-06-19", time: "03:00", homeCompetitor: "Mexico", awayCompetitor: "South Korea" },
  { group: "D", date: "2026-06-19", time: "21:00", homeCompetitor: "USA", awayCompetitor: "Australia" },
  { group: "C", date: "2026-06-20", time: "00:00", homeCompetitor: "Scotland", awayCompetitor: "Morocco" },
  { group: "C", date: "2026-06-20", time: "03:00", homeCompetitor: "Brazil", awayCompetitor: "Haiti" },
  { group: "D", date: "2026-06-20", time: "06:00", homeCompetitor: "TBD", awayCompetitor: "Paraguay" },
  { group: "F", date: "2026-06-20", time: "19:00", homeCompetitor: "Netherlands", awayCompetitor: "TBD" },
  { group: "E", date: "2026-06-20", time: "22:00", homeCompetitor: "Germany", awayCompetitor: "Ivory Coast" },
  { group: "E", date: "2026-06-21", time: "02:00", homeCompetitor: "Ecuador", awayCompetitor: "Curaçao" },
  { group: "F", date: "2026-06-21", time: "06:00", homeCompetitor: "Tunisia", awayCompetitor: "Japan" },
  { group: "H", date: "2026-06-21", time: "18:00", homeCompetitor: "Spain", awayCompetitor: "Saudi Arabia" },
  { group: "G", date: "2026-06-21", time: "21:00", homeCompetitor: "Belgium", awayCompetitor: "Iran" },
  { group: "H", date: "2026-06-22", time: "00:00", homeCompetitor: "Uruguay", awayCompetitor: "Cape Verde" },
  { group: "G", date: "2026-06-22", time: "03:00", homeCompetitor: "Iran", awayCompetitor: "New Zealand" },
  { group: "G", date: "2026-06-22", time: "03:00", homeCompetitor: "New Zealand", awayCompetitor: "Egypt" },
  { group: "J", date: "2026-06-22", time: "19:00", homeCompetitor: "Argentina", awayCompetitor: "Austria" },
  { group: "I", date: "2026-06-22", time: "23:00", homeCompetitor: "France", awayCompetitor: "TBD" },
  { group: "I", date: "2026-06-23", time: "02:00", homeCompetitor: "Norway", awayCompetitor: "Senegal" },
  { group: "J", date: "2026-06-23", time: "05:00", homeCompetitor: "Jordan", awayCompetitor: "Algeria" },
  { group: "K", date: "2026-06-23", time: "19:00", homeCompetitor: "Portugal", awayCompetitor: "Uzbekistan" },
  { group: "L", date: "2026-06-23", time: "22:00", homeCompetitor: "England", awayCompetitor: "Ghana" },
  { group: "L", date: "2026-06-24", time: "01:00", homeCompetitor: "Panama", awayCompetitor: "Croatia" },
  { group: "K", date: "2026-06-24", time: "04:00", homeCompetitor: "Colombia", awayCompetitor: "TBD" },
  { group: "B", date: "2026-06-24", time: "21:00", homeCompetitor: "Switzerland", awayCompetitor: "Canada" },
  { group: "B", date: "2026-06-24", time: "21:00", homeCompetitor: "TBD", awayCompetitor: "Qatar" },
  { group: "C", date: "2026-06-25", time: "00:00", homeCompetitor: "Morocco", awayCompetitor: "Haiti" },
  { group: "C", date: "2026-06-25", time: "00:00", homeCompetitor: "Scotland", awayCompetitor: "Brazil" },
  { group: "A", date: "2026-06-25", time: "03:00", homeCompetitor: "South Africa", awayCompetitor: "South Korea" },
  { group: "A", date: "2026-06-25", time: "03:00", homeCompetitor: "TBD", awayCompetitor: "Mexico" },
  { group: "E", date: "2026-06-25", time: "22:00", homeCompetitor: "Curaçao", awayCompetitor: "Ivory Coast" },
  { group: "E", date: "2026-06-25", time: "22:00", homeCompetitor: "Ecuador", awayCompetitor: "Germany" },
  { group: "F", date: "2026-06-26", time: "01:00", homeCompetitor: "Tunisia", awayCompetitor: "Netherlands" },
  { group: "F", date: "2026-06-26", time: "01:00", homeCompetitor: "Japan", awayCompetitor: "TBD" },
  { group: "D", date: "2026-06-26", time: "04:00", homeCompetitor: "TBD", awayCompetitor: "USA" },
  { group: "D", date: "2026-06-26", time: "04:00", homeCompetitor: "Paraguay", awayCompetitor: "Australia" },
  { group: "I", date: "2026-06-26", time: "21:00", homeCompetitor: "Norway", awayCompetitor: "France" },
  { group: "I", date: "2026-06-26", time: "21:00", homeCompetitor: "Senegal", awayCompetitor: "TBD" },
  { group: "H", date: "2026-06-27", time: "02:00", homeCompetitor: "Cape Verde", awayCompetitor: "Saudi Arabia" },
  { group: "H", date: "2026-06-27", time: "02:00", homeCompetitor: "Uruguay", awayCompetitor: "Spain" },
  { group: "G", date: "2026-06-27", time: "05:00", homeCompetitor: "New Zealand", awayCompetitor: "Belgium" },
  { group: "G", date: "2026-06-27", time: "05:00", homeCompetitor: "Egypt", awayCompetitor: "Iran" },
  { group: "L", date: "2026-06-27", time: "23:00", homeCompetitor: "Panama", awayCompetitor: "England" },
  { group: "L", date: "2026-06-27", time: "23:00", homeCompetitor: "Croatia", awayCompetitor: "Ghana" },
  { group: "K", date: "2026-06-28", time: "01:30", homeCompetitor: "Colombia", awayCompetitor: "Portugal" },
  { group: "K", date: "2026-06-28", time: "01:30", homeCompetitor: "TBD", awayCompetitor: "Uzbekistan" },
  { group: "J", date: "2026-06-28", time: "04:00", homeCompetitor: "Algeria", awayCompetitor: "Austria" },
  { group: "J", date: "2026-06-28", time: "04:00", homeCompetitor: "Jordan", awayCompetitor: "Argentina" },
];





export const MOCK_EXTRABETS: ExtraBet[] = [
  { 
    id: 'eb1', 
    tournamentId: 't1', 
    question: 'Vem vinner skytteligan?', 
    allowedValues: ['Mbappé', 'Kane', 'Isak', 'Ronaldo'], 
    requiresValue: false,
    deadline: '2024-06-20T00:00:00Z', 
    points: 50 
  },
  { 
    id: 'eb2', 
    tournamentId: 't1', 
    question: 'Hur långt går Sverige?', 
    allowedValues: ['Gruppspel', 'Åttondel', 'Kvartsfinal', 'Semifinal', 'Final'], 
    requiresValue: false,
    deadline: '2024-06-14T00:00:00Z', 
    points: 30 
  }
];
