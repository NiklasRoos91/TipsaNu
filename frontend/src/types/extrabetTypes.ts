export type CreateExtraBetOptionDto = {
  tournamentId: number;
  matchId?: number | null;
  name: string;
  description: string;
  points: number;
  expiresAt?: string | null;
  allowCustomChoice: boolean;
  choices: string[];
};

export type ExtraBetOptionForUser = ExtraBetOptionDto & {
  myBet?: ExtraBetDto | null;
};

export type ExtraBetOptionCorrectValueDto = {
  correctValueId: number;
  optionId: number;
  value: string;
};

export type CreateExtraBetDto = {
  value: string;
};

export type SetExtraBetOptionCorrectValuesDto = {
  values: string[];
};

export type ExtraBetOptionCorrectValuesResponse = boolean;

export type ExtraBetOptionChoiceDto = {
  choiceId: number;
  optionId: number;
  value: string;
};

export type ExtraBetOptionDto = {
  optionId: number;
  tournamentId: number;
  matchId?: number | null;
  name: string;
  description: string;
  points: number;
  expiresAt?: string | null;
  allowCustomChoice: boolean;
  status: string; // "Open" | "Closed" | "Cancelled" (string från backend)
  choices: ExtraBetOptionChoiceDto[];
};

export type ExtraBetDto = {
  extraBetId: number;
  optionId: number;
  choiceId?: number | null;
  value?: string | null;
  pointsAwarded?: number | null;
  submittedAt: string;
};
