import { useState } from "react";
import { extraBetService } from "../services/extraBetService";
import type { CreateExtraBetOptionDto, ExtraBetOptionDto } from "../types/extrabetTypes";

export function useCreateExtraBetOption() {
  const [data, setData] = useState<ExtraBetOptionDto | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const createExtraBetOption = async (dto: CreateExtraBetOptionDto) => {
    setIsLoading(true);
    setError(null);

    try {
      const result = await extraBetService.createExtraBetOption(dto);
      setData(result);
      return result;
    } catch (err: any) {
      const message =
        err?.response?.data?.message ||
        err?.message ||
        "Kunde inte skapa ExtraBet.";

      setError(message);
      throw err;
    } finally {
      setIsLoading(false);
    }
  };

  return {
    createExtraBetOption,
    data,
    isLoading,
    error,
  };
}
