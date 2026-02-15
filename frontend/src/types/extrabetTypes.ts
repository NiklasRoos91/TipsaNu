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
