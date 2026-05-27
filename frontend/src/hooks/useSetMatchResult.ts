import { useState } from "react";
import { Match } from "../types/matchTypes";
import { setMatchResult } from "../services/adminMatchService";

export const useSetMatchResult = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [updatedMatch, setUpdatedMatch] = useState<Match | null>(null);

  const handleSubmitResult = async (
    matchId: number,
    scoreHome: number,
    scoreAway: number
  ) => {
    setLoading(true);
    setError(null);

    try {
      const match = await setMatchResult(matchId, {
        scoreHome,
        scoreAway,
      });

      setUpdatedMatch(match);
      return match;
      } catch (err: any) {
      if (err.response?.data?.errorMessage) {
        setError(err.response.data.errorMessage);
      } else if (err.response?.data) {
        setError(typeof err.response.data === "string" ? err.response.data : "Ett oväntat fel uppstod.");
      } else {
        setError("Kunde inte spara resultat. Kontrollera din nätverksanslutning.");
      }
      throw err;
    } finally {
      setLoading(false);
    }
  };

  return {
    handleSubmitResult,
    loading,
    error,
    updatedMatch,
  };
};
