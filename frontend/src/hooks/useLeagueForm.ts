import { useState } from 'react';
import { createLeague, joinLeague } from '../services/api';
import { League } from '../types/types';

export const useLeagueForm = (tournamentId: string, onLeagueAdded: (l: League) => void) => {
  const [showCreate, setShowCreate] = useState(false);
  const [showJoin, setShowJoin] = useState(false);
  const [newLeagueName, setNewLeagueName] = useState('');
  const [joinCode, setJoinCode] = useState('');
  const [isProcessing, setIsProcessing] = useState(false);

  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newLeagueName.trim()) return;
    setIsProcessing(true);
    try {
      const l = await createLeague(newLeagueName, tournamentId);
      onLeagueAdded(l);
      setShowCreate(false);
      setNewLeagueName('');
    } catch (e) {
      alert("Kunde inte skapa liga.");
    } finally {
      setIsProcessing(false);
    }
  };

  const handleJoin = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!joinCode.trim()) return;
    setIsProcessing(true);
    try {
      const league = await joinLeague(joinCode);
      if (league) {
        if (league.tournamentId !== tournamentId) {
          alert("Denna kod tillhör en liga i en annan turnering.");
        } else {
          onLeagueAdded(league);
          setShowJoin(false);
          setJoinCode('');
          alert(`Välkommen till ${league.name}!`);
        }
      } else {
        alert("Ogiltig inbjudningskod.");
      }
    } catch (e) {
      alert("Ett fel uppstod.");
    } finally {
      setIsProcessing(false);
    }
  };

  return {
    showCreate, setShowCreate,
    showJoin, setShowJoin,
    newLeagueName, setNewLeagueName,
    joinCode, setJoinCode,
    isProcessing,
    handleCreate,
    handleJoin
  };
};