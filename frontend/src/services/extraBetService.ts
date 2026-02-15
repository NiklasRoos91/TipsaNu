import { api } from "./apiClient";
import type { CreateExtraBetOptionDto, ExtraBetOptionDto, ExtraBetOptionForUser, ExtraBetOptionCorrectValueDto } from "../types/extrabetTypes";

export const extraBetService = {
  createExtraBetOption: async (
    dto: CreateExtraBetOptionDto
  ): Promise<ExtraBetOptionDto> => {
    const response = await api.post<ExtraBetOptionDto>(
      "/admin/extrabets/options",
      dto
    );

    return response.data;
  },

    getOptionsForUser: async (tournamentId: number): Promise<ExtraBetOptionForUser[]> => {
    const response = await api.get<ExtraBetOptionForUser[]>(
      `/extrabets/options?tournamentId=${tournamentId}`
    );
    return response.data;
  },

  getCorrectValuesByOptionId: async (
    optionId: number
  ): Promise<ExtraBetOptionCorrectValueDto[]> => {
    const response = await api.get<ExtraBetOptionCorrectValueDto[]>(
      `/extrabets/${optionId}/correct-values`
    );
    return response.data;
  },
};

