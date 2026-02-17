import { api } from "./apiClient";
import type { CreateExtraBetOptionDto, ExtraBetOptionDto, ExtraBetOptionForUser, ExtraBetOptionCorrectValueDto, CreateExtraBetDto, SetExtraBetOptionCorrectValuesDto, ExtraBetOptionCorrectValuesResponse, ExtraBetDto } from "../types/extrabetTypes";

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

      createCorrectValues: async (
    optionId: number,
    dto: SetExtraBetOptionCorrectValuesDto
  ): Promise<ExtraBetOptionCorrectValuesResponse> => {
    const response = await api.post<ExtraBetOptionCorrectValuesResponse>(
      `/admin/extrabets/options/${optionId}/correct-values`,
      dto
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

  submitMyExtraBet: async (
    optionId: number,
    dto: CreateExtraBetDto
  ): Promise<ExtraBetDto> => {
    const response = await api.post<ExtraBetDto>(
      `/extrabets/${optionId}/mine`,
      dto
    );
    return response.data;
  },

  getExtraBetOptions: async (
    tournamentId: number,
    status: string = "all"
  ): Promise<ExtraBetOptionDto[]> => {
    const response = await api.get<ExtraBetOptionDto[]>(
      `/extrabets/options?tournamentId=${tournamentId}&status=${status}`
    );
    return response.data;
  },

  getMyExtraBetByOptionId: async (
    optionId: number
  ): Promise<ExtraBetDto | null> => {
    try {
      const response = await api.get<ExtraBetDto>(
        `/extrabets/options/${optionId}/me`
      );
      return response.data;
    } catch (error: any) {
      if (error.response?.status === 404) {
        return null; // ingen extra bet för användaren än
      }
      throw error;
    }
  },
};
