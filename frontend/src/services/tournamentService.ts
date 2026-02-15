import { api } from './apiClient';
import { Tournament, Group, GroupStanding } from '../types/tournamentTypes';
import { Match } from '../types/matchTypes';

export const getTournaments = async (): Promise<Tournament[]> => {
  try {
    const response = await api.get<Tournament[]>('/tournaments');
    return response.data;
  } catch (err: any) {
    console.error('Kunde inte hämta turneringar', err);
    throw new Error(err.response?.data?.message || 'Nätverksfel vid hämtning av turneringar');
  }
};

export const getTournament = async (tournamentId: number): Promise<Tournament> => {
  try {
    const response = await api.get<Tournament>(`/tournaments/${tournamentId}`);
    return response.data;
  } catch (err: any) {
    console.error(`Kunde inte hämta turnering ${tournamentId}`, err);
    throw new Error(err.response?.data?.message || 'Nätverksfel vid hämtning av turnering');
  }
};

export const getGroups = async (tournamentId: number): Promise<Group[]> => {
  try {
    const response = await api.get<Group[]>(`/tournaments/${tournamentId}/groups`);
    return response.data;
  } catch (err: any) {
    console.error(`Kunde inte hämta grupper för turnering ${tournamentId}`, err);
    throw new Error(err.response?.data?.message || 'Fel vid hämtning av grupper');
  }
};

export const getMatches = async (groupId: number): Promise<Match[]> => {
  try {
    const response = await api.get<Match[]>(`/groups/${groupId}/matches`);
    return response.data;
  } catch (err: any) {
    console.error(`Kunde inte hämta matcher för grupp ${groupId}`, err);
    throw new Error(err.response?.data?.message || 'Fel vid hämtning av matcher');
  }
};

export const getStandings = async (groupId: number): Promise<GroupStanding[]> => {
  try {
    const response = await api.get<GroupStanding[]>(`/groups/${groupId}/standings`);
    return response.data;
  } catch (err: any) {
    console.error(`Kunde inte hämta tabell för grupp ${groupId}`, err);
    throw new Error(err.response?.data?.message || 'Fel vid hämtning av tabell');
  }
};