import React, { useState } from "react";
import { FormButtons } from "../commons/FormButtons";
import type { SetExtraBetOptionCorrectValuesDto } from "../../types/extrabetTypes";
import { useCreateExtraBetCorrectValues } from "../../hooks/extraBets/useCreateExtraBetCorrectValues";

interface ExtraBetCorrectValuesFormProps {
  optionId: number;
  onCancel: () => void;
}

export const ExtraBetCorrectValuesForm: React.FC<ExtraBetCorrectValuesFormProps> = ({ optionId, onCancel }) => {
  const [correctValue, setCorrectValue] = useState<string>("");
  const { createCorrectValues, loading, error, success } = useCreateExtraBetCorrectValues(optionId);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!correctValue.trim()) return;

    await createCorrectValues({ correctValues: [correctValue.trim()] } as SetExtraBetOptionCorrectValuesDto);
    setCorrectValue("");
  };

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto space-y-6">
      <input
        type="text"
        value={correctValue}
        onChange={(e) => setCorrectValue(e.target.value)}
        placeholder="Skriv korrekt värde här..."
        className="w-full px-4 py-3 border-2 border-slate-200 rounded-xl focus:border-accent focus:ring-4 focus:ring-accent/10 outline-none transition-all font-medium text-slate-900"
        autoFocus
      />

      {error && <p className="text-red-500 text-sm">{error}</p>}
      {success && <p className="text-green-600 text-sm">Korrektvärde sparat!</p>}

      <FormButtons
        isSubmitting={loading}
        showSuccess={success}
        hasExistingData={false}
        onCancel={onCancel}
        saveLabel="Korrektvärde"
        successLabel="Sparat!"
      />
    </form>
  );
};
