import { useState } from 'react';
import { Match, CreateMatchDto } from '../../types/matchTypes';
import { createMatch } from '../../services/adminMatchService';

export const useCreateMatch = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);

  const handleCreateMatch = async (matchData: CreateMatchDto) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      const newMatch = await createMatch(matchData);
      setSuccess(true);
      return newMatch;
    } catch (err: any) {
      setError(err.message || 'Kunde inte skapa matchen.');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  return {
    handleCreateMatch,
    loading,
    error,
    success,
    setSuccess
  };
};