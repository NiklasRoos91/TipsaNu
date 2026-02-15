import { api } from "./apiClient";
import type { CreateExtraBetOptionDto, ExtraBetOptionDto } from "../types/extrabetTypes";

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
};
