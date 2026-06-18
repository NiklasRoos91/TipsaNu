import React, { useState } from "react";
import { ScoreInputForm } from "../commons/inputs/ScoreInputForm";

interface MatchResultFormProps {
  match: { homeCompetitorName: string; awayCompetitorName: string };
  onSubmit: (homeScore: number, awayScore: number) => Promise<void>;
  onCancel: () => void;
  hasExistingData?: boolean;
}

export const MatchResultForm: React.FC<MatchResultFormProps> = ({ match, onSubmit, onCancel, hasExistingData }) => {
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
      hasExistingData={hasExistingData}
      saveLabel="Resultat"
      successLabel="Resultat sparat!"
      isSubmitting={false}
    />
  );
};
