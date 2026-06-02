import { useState } from 'react';
import { MatchStatusEnum } from '../../types/enums/matchEnums';
import { updateMatchStatus } from '../../services/adminMatchService';

export const useUpdateMatchStatus = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);

  const handleUpdateStatus = async (matchId: number, status: MatchStatusEnum) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      const updatedMatch = await updateMatchStatus(matchId, status);
      setSuccess(true);
      return updatedMatch;
    } catch (err: any) {
      const backendError = 
        err.response?.data?.ErrorMessages?.[0] || 
        err.response?.data?.ErrorMessage || 
        err.response?.data?.message ||
        (typeof err.response?.data === 'string' ? err.response.data : null);

      setError(backendError || 'Kunde inte uppdatera matchstatus.');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  return {
    handleUpdateStatus,
    loading,
    error,
    success,
    setSuccess
  };
};