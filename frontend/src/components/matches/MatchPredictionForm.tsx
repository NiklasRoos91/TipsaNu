import React, { useState } from "react";
import { ScoreInputForm } from "../commons/inputs/ScoreInputForm";
import { MatchPredictionDto } from "../../types/matchTypes";

interface MatchPredictionFormProps {
  match: { homeCompetitorName: string; awayCompetitorName: string };
  prediction?: MatchPredictionDto;
  onSubmit: (homeScore: number, awayScore: number) => Promise<void>;
  onCancel: () => void;
}

export const MatchPredictionForm: React.FC<MatchPredictionFormProps> = ({ match, prediction, onSubmit, onCancel }) => {
  const [homeScore, setHomeScore] = useState<number>(prediction?.predictedHomeScore ?? 0);
  const [awayScore, setAwayScore] = useState<number>(prediction?.predictedAwayScore ?? 0);

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
      saveLabel="Tips"
      successLabel="Tippat & Klart!"
      isSubmitting={false}
    />
  );
};
