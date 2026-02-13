import React, { useState } from "react";
import { ScoreInputForm } from "../commons/inputs/ScoreInputForm";

interface MatchResultFormProps {
  match: { homeCompetitorName: string; awayCompetitorName: string };
  onSubmit: (homeScore: number, awayScore: number) => Promise<void>;
  onCancel: () => void;
}

export const MatchResultForm: React.FC<MatchResultFormProps> = ({ match, onSubmit, onCancel }) => {
const [homeScore, setHomeScore] = useState<number>(0);
const [awayScore, setAwayScore] = useState<number>(0);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await onSubmit(Number(homeScore), Number(awayScore));
  };

  return (
    <ScoreInputForm
      homeTeamName={match.homeCompetitorName}
      awayTeamName={match.awayCompetitorName}
      homeScore={homeScore}
      awayScore={awayScore}
      onHomeChange={setHomeScore}
      onAwayChange={setAwayScore}
      onSubmit={handleSubmit}
      onCancel={onCancel}
      saveLabel="Resultat"
      successLabel="Resultat sparat!"
      isSubmitting={false}
    />
  );
};
