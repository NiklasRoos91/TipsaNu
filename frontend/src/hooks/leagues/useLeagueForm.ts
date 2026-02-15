import { useState } from 'react';
import type { CreateLeagueDto, CreatedLeagueWithMemberDto, JoinLeagueDto, LeagueMemberDto } from '../../types/leagueTypes.ts';
import { useCreateLeague } from './useCreateLeague.ts'; 
import { useJoinLeague } from './useJoinLeague.ts';

type League = {
  leagueId: string;
  name: string;
  tournamentId: string;
};

export const useLeagueForm = (
  tournamentId: string,
  onLeagueAdded: (l: CreatedLeagueWithMemberDto) => void,
  onLeagueJoined?: (m: LeagueMemberDto) => void
) => {
  const [showCreate, setShowCreate] = useState(false);
  const [showJoin, setShowJoin] = useState(false);
  const [newLeagueName, setNewLeagueName] = useState('');
  const [joinCode, setJoinCode] = useState('');
  const [isProcessing, setIsProcessing] = useState(false);

  const { execute: createLeague } = useCreateLeague();
  const { execute: joinLeague } = useJoinLeague();

  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newLeagueName.trim()) return;

    setIsProcessing(true);
    try {
      const dto: CreateLeagueDto = {
        tournamentId: Number(tournamentId),
        name: newLeagueName,
        description: '', 
        maxMembers: 50,
      };

      const result = await createLeague(dto);
      if (result) {
        onLeagueAdded(result);
        setShowCreate(false);
        setNewLeagueName('');
      } else {
        alert('Kunde inte skapa ligan. Försök igen.');
      }
    } catch (e) {
      alert('Kunde inte skapa ligan. Försök igen.');
    } finally {
      setIsProcessing(false);
    }
  };

  const handleJoin = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!joinCode.trim()) return;

    setIsProcessing(true);
    try {
      const dto: JoinLeagueDto = { invitationCode: joinCode.toUpperCase() };
      const result = await joinLeague(Number(tournamentId), dto);
      if (result) {
        onLeagueJoined?.(result);
        setShowJoin(false);
        setJoinCode('');
      } else {
        alert('Kunde inte gå med i ligan. Kontrollera koden.');
      }
    } finally {
      setIsProcessing(false);
    }
  };


  return {
    showCreate,
    setShowCreate,
    showJoin,
    setShowJoin,
    newLeagueName,
    setNewLeagueName,
    joinCode,
    setJoinCode,
    isProcessing,
    handleCreate,
    handleJoin,
  };
};