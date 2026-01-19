
import { Match, MatchStatus, Tournament, User, League, Post, Notification, ExtraBet, GroupStanding, Team, TournamentTemplate, TiebreakerCriterion } from '../types/types';

export const MOCK_USERS: User[] = [
  {
    id: 'u1',
    username: 'user1',
    email: 'user1@tipsanu.se',
    displayName: 'User One',
    points: 1250,
    bio: 'Älskar fotboll och statistik.',
    avatarUrl: 'https://picsum.photos/200/200'
  },
  {
    id: 'admin1',
    username: 'admin',
    email: 'admin@tipsanu.se',
    displayName: 'Admin User',
    points: 9999,
    bio: 'Jag bestämmer här.',
    avatarUrl: 'https://ui-avatars.com/api/?name=Admin+User&background=0D8ABC&color=fff'
  }
];

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

export const MOCK_TOURNAMENTS: Tournament[] = [
  {
    id: 't1',
    name: 'Fotbolls-EM 2024',
    startDate: '2024-06-14T19:00:00Z',
    endDate: '2024-07-14T20:00:00Z',
    status: 'ACTIVE',
    bannerUrl: 'https://picsum.photos/800/300?random=1'
  },
  {
    id: 't2',
    name: 'VM 2026',
    startDate: '2026-06-11T18:00:00Z',
    endDate: '2026-07-19T20:00:00Z',
    status: 'UPCOMING',
    bannerUrl: 'https://images.unsplash.com/photo-1574629810360-7efbbe195018?q=80&w=1200&auto=format&fit=crop'
  }
];

const TEAMS: Record<string, Team> = {
  'Mexico': { id: 'MX', name: 'Mexico', flagUrl: 'https://flagcdn.com/w160/mx.png' },
  'South Africa': { id: 'ZA', name: 'Sydafrika', flagUrl: 'https://flagcdn.com/w160/za.png' },
  'South Korea': { id: 'KR', name: 'Sydkorea', flagUrl: 'https://flagcdn.com/w160/kr.png' },
  'Canada': { id: 'CA', name: 'Kanada', flagUrl: 'https://flagcdn.com/w160/ca.png' },
  'USA': { id: 'US', name: 'USA', flagUrl: 'https://flagcdn.com/w160/us.png' },
  'Paraguay': { id: 'PY', name: 'Paraguay', flagUrl: 'https://flagcdn.com/w160/py.png' },
  'Qatar': { id: 'QA', name: 'Qatar', flagUrl: 'https://flagcdn.com/w160/qa.png' },
  'Switzerland': { id: 'CH', name: 'Schweiz', flagUrl: 'https://flagcdn.com/w160/ch.png' },
  'Brazil': { id: 'BR', name: 'Brasilien', flagUrl: 'https://flagcdn.com/w160/br.png' },
  'Morocco': { id: 'MA', name: 'Marocko', flagUrl: 'https://flagcdn.com/w160/ma.png' },
  'Haiti': { id: 'HT', name: 'Haiti', flagUrl: 'https://flagcdn.com/w160/ht.png' },
  'Scotland': { id: 'SCT', name: 'Skottland', flagUrl: 'https://flagcdn.com/w160/gb-sct.png' },
  'Australia': { id: 'AU', name: 'Australien', flagUrl: 'https://flagcdn.com/w160/au.png' },
  'Germany': { id: 'DE', name: 'Tyskland', flagUrl: 'https://flagcdn.com/w160/de.png' },
  'Curaçao': { id: 'CW', name: 'Curaçao', flagUrl: 'https://flagcdn.com/w160/cw.png' },
  'Netherlands': { id: 'NL', name: 'Nederländerna', flagUrl: 'https://flagcdn.com/w160/nl.png' },
  'Japan': { id: 'JP', name: 'Japan', flagUrl: 'https://flagcdn.com/w160/jp.png' },
  'Ivory Coast': { id: 'CI', name: 'Elfenbenskusten', flagUrl: 'https://flagcdn.com/w160/ci.png' },
  'Ecuador': { id: 'EC', name: 'Ecuador', flagUrl: 'https://flagcdn.com/w160/ec.png' },
  'Tunisia': { id: 'TN', name: 'Tunisien', flagUrl: 'https://flagcdn.com/w160/tn.png' },
  'Spain': { id: 'ES', name: 'Spanien', flagUrl: 'https://flagcdn.com/w160/es.png' },
  'Cape Verde': { id: 'CV', name: 'Kap Verde', flagUrl: 'https://flagcdn.com/w160/cv.png' },
  'Belgium': { id: 'BE', name: 'Belgien', flagUrl: 'https://flagcdn.com/w160/be.png' },
  'Egypt': { id: 'EG', name: 'Egypten', flagUrl: 'https://flagcdn.com/w160/eg.png' },
  'Saudi Arabia': { id: 'SA', name: 'Saudiarabien', flagUrl: 'https://flagcdn.com/w160/sa.png' },
  'Uruguay': { id: 'UY', name: 'Uruguay', flagUrl: 'https://flagcdn.com/w160/uy.png' },
  'Iran': { id: 'IR', name: 'Iran', flagUrl: 'https://flagcdn.com/w160/ir.png' },
  'New Zealand': { id: 'NZ', name: 'Nya Zeeland', flagUrl: 'https://flagcdn.com/w160/nz.png' },
  'France': { id: 'FR', name: 'Frankrike', flagUrl: 'https://flagcdn.com/w160/fr.png' },
  'Senegal': { id: 'SN', name: 'Senegal', flagUrl: 'https://flagcdn.com/w160/sn.png' },
  'Norway': { id: 'NO', name: 'Norge', flagUrl: 'https://flagcdn.com/w160/no.png' },
  'Argentina': { id: 'AR', name: 'Argentina', flagUrl: 'https://flagcdn.com/w160/ar.png' },
  'Algeria': { id: 'DZ', name: 'Algeriet', flagUrl: 'https://flagcdn.com/w160/dz.png' },
  'Austria': { id: 'AT', name: 'Österrike', flagUrl: 'https://flagcdn.com/w160/at.png' },
  'Jordan': { id: 'JO', name: 'Jordanien', flagUrl: 'https://flagcdn.com/w160/jo.png' },
  'Portugal': { id: 'PT', name: 'Portugal', flagUrl: 'https://flagcdn.com/w160/pt.png' },
  'England': { id: 'ENG', name: 'England', flagUrl: 'https://flagcdn.com/w160/gb-eng.png' },
  'Croatia': { id: 'HR', name: 'Kroatien', flagUrl: 'https://flagcdn.com/w160/hr.png' },
  'Ghana': { id: 'GH', name: 'Ghana', flagUrl: 'https://flagcdn.com/w160/gh.png' },
  'Panama': { id: 'PA', name: 'Panama', flagUrl: 'https://flagcdn.com/w160/pa.png' },
  'Uzbekistan': { id: 'UZ', name: 'Uzbekistan', flagUrl: 'https://flagcdn.com/w160/uz.png' },
  'Colombia': { id: 'CO', name: 'Colombia', flagUrl: 'https://flagcdn.com/w160/co.png' },
  'TBD': { id: 'TBD', name: 'Kvalvinnare', flagUrl: 'https://flagcdn.com/w160/un.png' },
  'Sverige': { id: 'SE', name: 'Sverige', flagUrl: 'https://flagcdn.com/w160/se.png' },
  'Danmark': { id: 'DK', name: 'Danmark', flagUrl: 'https://flagcdn.com/w160/dk.png' },
  'Italien': { id: 'IT', name: 'Italien', flagUrl: 'https://flagcdn.com/w160/it.png' }
};

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

export const MOCK_MATCHES: Match[] = [
  {
    id: 'm1',
    tournamentId: 't1',
    homeTeam: TEAMS['Sverige'],
    awayTeam: TEAMS['Danmark'],
    startTime: new Date(Date.now() - 3600000).toISOString(),
    status: MatchStatus.LIVE,
    homeScore: 1,
    awayScore: 1,
    group: 'Grupp A'
  },
  {
    id: 'm2',
    tournamentId: 't1',
    homeTeam: TEAMS['Germany'],
    awayTeam: TEAMS['France'],
    startTime: new Date(Date.now() + 86400000).toISOString(),
    status: MatchStatus.SCHEDULED,
    group: 'Grupp A'
  },
  {
    id: 'm3',
    tournamentId: 't1',
    homeTeam: TEAMS['Spain'],
    awayTeam: TEAMS['Italien'],
    startTime: new Date(Date.now() - 172800000).toISOString(),
    status: MatchStatus.FINISHED,
    homeScore: 2,
    awayScore: 0,
    group: 'Grupp B'
  },
  ...RAW_MATCHES_2026.map((m, idx) => ({
    id: `m2026_${idx}`,
    tournamentId: 't2',
    homeTeam: TEAMS[m.homeCompetitor] || TEAMS['TBD'],
    awayTeam: TEAMS[m.awayCompetitor] || TEAMS['TBD'],
    startTime: `${m.date}T${m.time}:00Z`,
    status: MatchStatus.SCHEDULED,
    group: `Grupp ${m.group}`
  }))
];

// Helper to generate empty mock standings for groups A-L
const generateMockStandings = () => {
  const standings: Record<string, GroupStanding[]> = {
    'Grupp A': [
      { rank: 1, competitor: TEAMS['Mexico'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
      { rank: 2, competitor: TEAMS['South Africa'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
      { rank: 3, competitor: TEAMS['South Korea'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
      { rank: 4, competitor: TEAMS['TBD'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
    ],
    'Grupp B': [
      { rank: 1, competitor: TEAMS['Canada'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
      { rank: 2, competitor: TEAMS['Qatar'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
      { rank: 3, competitor: TEAMS['Switzerland'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
      { rank: 4, competitor: TEAMS['TBD'], played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
    ],
  };

  const groups = "CDEFGHIJKLMNOPJKL".split("");
  groups.forEach(g => {
    const gn = `Grupp ${g}`;
    if (!standings[gn]) {
      standings[gn] = [
        { rank: 1, competitor: { id: `T1${g}`, name: `Team 1 ${g}`, flagUrl: 'https://flagcdn.com/w160/un.png' }, played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
        { rank: 2, competitor: { id: `T2${g}`, name: `Team 2 ${g}`, flagUrl: 'https://flagcdn.com/w160/un.png' }, played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
        { rank: 3, competitor: { id: `T3${g}`, name: `Team 3 ${g}`, flagUrl: 'https://flagcdn.com/w160/un.png' }, played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
        { rank: 4, competitor: { id: `T4${g}`, name: `Team 4 ${g}`, flagUrl: 'https://flagcdn.com/w160/un.png' }, played: 0, wins: 0, draws: 0, losses: 0, goalsFor: 0, goalsAgainst: 0, goalDifference: 0, points: 0 },
      ];
    }
  });

  return standings;
};

export const MOCK_STANDINGS: Record<string, GroupStanding[]> = generateMockStandings();

export const MOCK_LEAGUES: League[] = [
  { id: 'l1', tournamentId: 't1', name: 'Jobbkompisarna', ownerId: 'u2', code: 'JOBB123', membersCount: 15, description: 'Vi på kontoret' },
  { id: 'l2', tournamentId: 't1', name: 'Bästa Polarna', ownerId: 'u1', code: 'POLARE', membersCount: 6, description: 'Endast seriösa tips' }
];

export const MOCK_POSTS: Post[] = [
  { id: 'p1', leagueId: 'l1', userId: 'u2', username: 'Bossen', content: 'Kom igen nu, glöm inte tippa innan fredag!', createdAt: '2024-06-10T10:00:00Z', likes: 3 },
  { id: 'p2', leagueId: 'l1', userId: 'u1', username: 'user1', content: 'Sverige vinner lätt.', createdAt: '2024-06-10T11:00:00Z', likes: 1 }
];

export const MOCK_NOTIFICATIONS: Notification[] = [
  { id: 'n1', userId: 'u1', type: 'POINTS_AWARDED', title: 'Poäng utdelade', message: 'Du fick 10 poäng för matchen Spanien - Italien', read: false, createdAt: new Date().toISOString(), link: '/matches/m3' },
  { id: 'n2', userId: 'u1', type: 'MATCH_STARTED', title: 'Matchstart', message: 'Sverige - Danmark börjar nu!', read: true, createdAt: new Date(Date.now() - 3600000).toISOString(), link: '/matches/m1' }
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
