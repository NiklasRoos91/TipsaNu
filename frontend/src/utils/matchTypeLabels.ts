import { MatchTypeEnum } from '../types/enums/matchEnums';

export const knockoutLabels: Partial<Record<MatchTypeEnum, string>> = {
  [MatchTypeEnum.RoundOf16]: 'Åttondelsfinal',
  [MatchTypeEnum.QuarterFinal]: 'Kvartsfinal',
  [MatchTypeEnum.SemiFinal]: 'Semifinal',
  [MatchTypeEnum.Final]: 'Final',
  [MatchTypeEnum.ThirdPlace]: 'Bronsmatch',
  [MatchTypeEnum.CustomKnockout]: 'Slutspel',
};

const knockoutDefiniteLabels: Partial<Record<MatchTypeEnum, string>> = {
  [MatchTypeEnum.RoundOf16]: 'åttondelsfinalen',
  [MatchTypeEnum.QuarterFinal]: 'kvartsfinalen',
  [MatchTypeEnum.SemiFinal]: 'semifinalen',
  [MatchTypeEnum.Final]: 'finalen',
  [MatchTypeEnum.ThirdPlace]: 'bronsmatchen',
  [MatchTypeEnum.CustomKnockout]: 'slutspelet',
};

export const formatGroupName = (name: string): string =>
  name.replace(/^Group\s+/i, 'Grupp ');

export const getFilterChipLabel = (filter: string | MatchTypeEnum): string => {
  if (typeof filter === 'number') {
    return knockoutLabels[filter] ?? 'Slutspel';
  }
  return formatGroupName(filter);
};

export const getFilterInLabel = (filter: string | MatchTypeEnum): string => {
  if (typeof filter === 'number') {
    return knockoutDefiniteLabels[filter] ?? 'slutspelet';
  }
  return formatGroupName(filter);
};

export const matchCountText = (count: number): string =>
  `${count} ${count === 1 ? 'match' : 'matcher'}`;
