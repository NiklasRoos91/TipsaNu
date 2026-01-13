
import { MOCK_USERS, MOCK_TOURNAMENTS, MOCK_MATCHES, MOCK_LEAGUES, MOCK_POSTS, MOCK_NOTIFICATIONS, MOCK_EXTRABETS, MOCK_STANDINGS, MOCK_TEMPLATES } from './mockData';
import { User, Tournament, Match, Prediction, League, Post, Notification, ExtraBet, ExtraBetPrediction, GroupStanding, TournamentTemplate } from '../types';

// --- IN-MEMORY STATE (Acts as a database during the session) ---
let users = [...MOCK_USERS];
let leagues = [...MOCK_LEAGUES];
let posts = [...MOCK_POSTS];
let predictions: Prediction[] = [
  { id: 'p1', userId: 'u1', matchId: 'm3', homeScore: 2, awayScore: 1, pointsAwarded: 10 }
];
let sessionTournaments = [...MOCK_TOURNAMENTS];
let sessionTemplates = [...MOCK_TEMPLATES];
let extraBets = [...MOCK_EXTRABETS];
let extraBetPredictions: ExtraBetPrediction[] = [];
let notifications = [...MOCK_NOTIFICATIONS];
let currentUser: User | null = null;

const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

const addNotification = (userId: string, title: string, message: string, link?: string) => {
  const n: Notification = {
    id: `n_${Date.now()}_${Math.random()}`,
    userId,
    type: 'PREDICTION_MADE',
    title,
    message,
    read: false,
    createdAt: new Date().toISOString(),
    link
  };
  notifications.unshift(n);
};

// --- Auth Services ---
export const login = async (emailOrUsername: string, password: string): Promise<{ user: User; token: string }> => {
  await delay(800);
  const user = users.find(u => u.email === emailOrUsername || u.username === emailOrUsername);
  if (!user) throw new Error('Invalid credentials');
  currentUser = user;
  const token = `mock-token-${user.id}-${user.username}`; 
  return { user, token };
};

export const register = async (data: any): Promise<{ user: User; token: string }> => {
  await delay(1000);
  const newUser: User = {
    id: `u_${Date.now()}`,
    username: data.username,
    email: data.email,
    displayName: data.username, 
    points: 0,
    bio: '',
    avatarUrl: `https://ui-avatars.com/api/?name=${data.username}&background=random`
  };
  users.push(newUser);
  currentUser = newUser;
  return { user: newUser, token: `mock-token-${newUser.id}` };
};

export const verifyToken = async (token: string): Promise<User | null> => {
  await delay(200);
  if (token.includes('admin')) {
    currentUser = users.find(u => u.username === 'admin') || null;
    return currentUser;
  }
  const foundUser = users.find(u => token.includes(u.id) || token.includes(u.username));
  if (foundUser) {
    currentUser = foundUser;
    return foundUser;
  }
  return null;
};

export const updateProfile = async (data: Partial<User>): Promise<User> => {
  await delay(500);
  if (!currentUser) throw new Error("Not authenticated");
  const index = users.findIndex(u => u.id === currentUser!.id);
  if (index !== -1) {
    users[index] = { ...users[index], ...data };
    currentUser = users[index];
    return currentUser;
  }
  throw new Error("User not found");
};

// --- Tournament & Template Services ---
export const getTournaments = async (): Promise<Tournament[]> => {
  await delay(500);
  return sessionTournaments;
};

export const getTournament = async (id: string): Promise<Tournament | undefined> => {
  await delay(300);
  return sessionTournaments.find(t => t.id === id);
};

export const createTournament = async (data: Omit<Tournament, 'id' | 'status'>): Promise<Tournament> => {
  await delay(1000);
  const newTournament: Tournament = {
    ...data,
    id: `t_${Date.now()}`,
    status: 'UPCOMING'
  };
  sessionTournaments.push(newTournament);
  return newTournament;
};

export const getTournamentTemplates = async (): Promise<TournamentTemplate[]> => {
  await delay(600);
  return sessionTemplates;
};

export const createTournamentTemplate = async (data: Omit<TournamentTemplate, 'id'>): Promise<TournamentTemplate> => {
  await delay(800);
  const newTemplate: TournamentTemplate = {
    ...data,
    id: `tpl_${Date.now()}`
  };
  sessionTemplates.unshift(newTemplate);
  return newTemplate;
};

// --- Match Services ---
export const getMatches = async (tournamentId: string): Promise<Match[]> => {
  await delay(600);
  return MOCK_MATCHES.filter(m => m.tournamentId === tournamentId);
};

export const getMatch = async (id: string): Promise<Match | undefined> => {
  await delay(400);
  return MOCK_MATCHES.find(m => m.id === id);
};

export const getGroupStandings = async (tournamentId: string, groupName: string): Promise<GroupStanding[]> => {
  await delay(400);
  return MOCK_STANDINGS[groupName] || [];
};

// --- Prediction Services ---
export const submitPrediction = async (matchId: string, home: number, away: number): Promise<Prediction> => {
  await delay(400);
  if (!currentUser) throw new Error("Not authenticated");
  const existingIndex = predictions.findIndex(p => p.matchId === matchId && p.userId === currentUser!.id);
  const newPrediction: Prediction = {
    id: existingIndex !== -1 ? predictions[existingIndex].id : `pred_${Date.now()}`,
    userId: currentUser.id,
    matchId,
    homeScore: home,
    awayScore: away,
    pointsAwarded: undefined
  };
  if (existingIndex !== -1) {
    predictions[existingIndex] = newPrediction;
  } else {
    predictions.push(newPrediction);
  }
  const match = MOCK_MATCHES.find(m => m.id === matchId);
  const matchName = match ? `${match.homeTeam.name}-${match.awayTeam.name}` : 'matchen';
  addNotification(currentUser.id, 'Tippning sparad', `Du tippade ${home}-${away} i ${matchName}.`, `/matches/${matchId}`);
  return newPrediction;
};

export const getMyPrediction = async (matchId: string): Promise<Prediction | null> => {
  await delay(300);
  if (!currentUser) return null;
  return predictions.find(p => p.matchId === matchId && p.userId === currentUser!.id) || null;
};

export const getMyPredictionsForTournament = async (tournamentId: string): Promise<Prediction[]> => {
    await delay(500);
    if (!currentUser) return [];
    const tournamentMatchIds = MOCK_MATCHES.filter(m => m.tournamentId === tournamentId).map(m => m.id);
    return predictions.filter(p => p.userId === currentUser!.id && tournamentMatchIds.includes(p.matchId));
}

// --- League Services ---
export const getMyLeagues = async (tournamentId?: string): Promise<League[]> => {
  await delay(400);
  if (tournamentId) {
    return leagues.filter(l => l.tournamentId === tournamentId);
  }
  return leagues;
};

export const getLeague = async (id: string): Promise<League | undefined> => {
  await delay(300);
  return leagues.find(l => l.id === id);
};

export const getLeagueMembers = async (leagueId: string) => {
  await delay(400);
  return [
    { id: '1', name: 'Test User', points: 1250 },
    { id: '2', name: 'Bossen', points: 1100 },
    { id: '3', name: 'Anna K', points: 950 },
    { id: '4', name: 'Sportfånen', points: 880 },
    { id: '5', name: 'User One', points: 1250 },
  ];
};

export const createLeague = async (name: string, tournamentId: string): Promise<League> => {
  await delay(600);
  if (!currentUser) throw new Error("Not authenticated");
  const newLeague: League = {
    id: `l_${Date.now()}`,
    name,
    tournamentId,
    ownerId: currentUser.id,
    code: `CODE${Math.floor(Math.random() * 1000)}`,
    membersCount: 1,
    description: 'Ny nyskapad liga'
  };
  leagues.push(newLeague);
  addNotification(currentUser.id, 'Liga skapad', `Du har skapat ligan ${name}`, `/tournaments/${tournamentId}/leagues/${newLeague.id}`);
  return newLeague;
};

export const joinLeague = async (code: string): Promise<League | undefined> => {
  await delay(500);
  const league = leagues.find(l => l.code === code);
  if (league) {
    league.membersCount += 1;
    if (currentUser) {
        addNotification(currentUser.id, 'Gick med i liga', `Du har gått med i ${league.name}`, `/tournaments/${league.tournamentId}/leagues/${league.id}`);
    }
    return league;
  }
  return undefined;
};

export const getLeaguePosts = async (leagueId: string): Promise<Post[]> => {
  await delay(400);
  return posts.filter(p => p.leagueId === leagueId).sort((a,b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
};

export const createPost = async (leagueId: string, content: string): Promise<Post> => {
  await delay(300);
  if (!currentUser) throw new Error("Not authenticated");
  const newPost: Post = {
    id: `p_${Date.now()}`,
    leagueId,
    userId: currentUser.id,
    username: currentUser.username,
    content,
    createdAt: new Date().toISOString(),
    likes: 0
  };
  posts.unshift(newPost);
  return newPost;
};

// --- Extra Bets ---
export const getExtraBets = async (tournamentId: string): Promise<ExtraBet[]> => {
  await delay(400);
  return extraBets.filter(e => e.tournamentId === tournamentId);
};

export const createExtraBet = async (data: Omit<ExtraBet, 'id'>): Promise<ExtraBet> => {
  await delay(500);
  const newBet: ExtraBet = { ...data, id: `eb_${Date.now()}` };
  extraBets.unshift(newBet);
  return newBet;
};

export const submitExtraBet = async (extraBetId: string, option: string): Promise<ExtraBetPrediction> => {
  await delay(400);
  if (!currentUser) throw new Error("Not authenticated");
  const existingIndex = extraBetPredictions.findIndex(e => e.extraBetId === extraBetId && e.userId === currentUser!.id);
  const newBet: ExtraBetPrediction = {
      id: existingIndex !== -1 ? extraBetPredictions[existingIndex].id : `ebp_${Date.now()}`,
      extraBetId,
      userId: currentUser.id,
      selectedOption: option,
      createdAt: new Date().toISOString()
  };
  if (existingIndex !== -1) {
      extraBetPredictions[existingIndex] = newBet;
  } else {
      extraBetPredictions.push(newBet);
  }
  const q = extraBets.find(e => e.id === extraBetId);
  if (q) {
      addNotification(currentUser.id, 'Extra Bet sparat', `Du tippade "${option}" på frågan: ${q.question}`, '/extrabets');
  }
  return newBet;
};

export const getMyExtraBetPredictions = async (tournamentId: string): Promise<ExtraBetPrediction[]> => {
    await delay(300);
    if (!currentUser) return [];
    const tournamentBetIds = extraBets.filter(e => e.tournamentId === tournamentId).map(e => e.id);
    return extraBetPredictions.filter(ep => ep.userId === currentUser!.id && tournamentBetIds.includes(ep.extraBetId));
};

// --- Notifications ---
export const getNotifications = async (): Promise<Notification[]> => {
  await delay(300);
  if (!currentUser) return [];
  return notifications
    .filter(n => n.userId === currentUser?.id || n.userId === 'all')
    .sort((a,b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime());
};
