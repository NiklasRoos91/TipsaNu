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

export type ExtraBetOptionDto = {
  optionId: number;
  tournamentId: number;
  matchId?: number | null;
  name: string;
  description: string;
  points: number;
  expiresAt?: string | null;
  allowCustomChoice: boolean;
  choices: string[];
};

export type ExtraBetOptionForUser = {
  optionId: number;
  name: string;
  description: string;
  points: number;
  expiresAt?: string | null;
  allowCustomChoice: boolean;
  choices: string[];
  myBet?: {
    extraBetId: number;
    value: string;
    pointsAwarded?: number | null;
    submittedAt: string;
  } | null;
};

export type ExtraBetOptionCorrectValueDto = {
  correctValueId: number;
  optionId: number;
  value: string;
};

export type CreateExtraBetDto = {
  value: string;
};

export type ExtraBetForUserDto = {
  extraBetId: number;
  value: string;
  pointsAwarded?: number | null;
  submittedAt: string;
};

export type SetExtraBetOptionCorrectValuesDto = {
  correctValues: string[];
};

export type ExtraBetOptionCorrectValuesResponse = boolean;
