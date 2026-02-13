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
    } catch (err) {
      setError("Kunde inte spara resultat.");
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
