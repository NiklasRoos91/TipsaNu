import React, { useState } from 'react';
import { Shield } from 'lucide-react';
import { useCreateExtraBetOption } from '../../hooks/useCreateExtraBetOption';
import { FormButtons } from '../commons/FormButtons';
import { ErrorMessage } from '../commons/ErrorMessage';

interface ExtraBetOptionFormProps {
  tournamentId: number | string;
  onCreated: () => void;
  onCancel: () => void;
}

export const ExtraBetOptionForm: React.FC<ExtraBetOptionFormProps> = ({
  tournamentId,
  onCreated,
  onCancel
}) => {
  const { createExtraBetOption, isLoading } = useCreateExtraBetOption();
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [createSuccess, setCreateSuccess] = useState(false);

  const [formData, setFormData] = useState({
    question: '',
    description: '',
    points: 10,
    deadline: '',
    optionsString: '',
    allowCustom: false
  });

  const [showOptionsInput, setShowOptionsInput] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!formData.question || !formData.deadline) {
      setErrorMessage('Fyll i alla obligatoriska fält.');
      return;
    }

    if (!formData.allowCustom && !showOptionsInput) {
      setErrorMessage('Du måste kryssa i minst en av checkboxarna: Tillåt fritext eller Visa förvalda alternativ.');
      return;
    }

    const choices = formData.optionsString
      .split(',')
      .map(s => s.trim())
      .filter(Boolean);

    if (showOptionsInput && choices.length === 0) {
      setErrorMessage('Ange minst ett alternativ.');
      return;
    }

    try {
      await createExtraBetOption({
        tournamentId: Number(tournamentId),
        matchId: null,
        name: formData.question,
        description: formData.description,
        points: formData.points,
        expiresAt: formData.deadline,
        allowCustomChoice: formData.allowCustom,
        choices
      });

      // Reset form
      setTimeout(() => {
        setFormData({
          question: '',
          description: '',
          points: 10,
          deadline: '',
          optionsString: '',
          allowCustom: false
        });
        setShowOptionsInput(false);
        setCreateSuccess(false);
        onCreated();
      }, 1200);

    } catch (err) {
      console.error(err);
      alert('Kunde inte skapa extrabet-option.');
    }
  };

  return (
    <div className="bg-white border border-slate-200 rounded-2xl p-6 shadow-sm animate-fade-in max-w-lg">
      <div className="flex items-center gap-2 mb-4 text-primary font-bold">
        <Shield size={20} className="text-purple-600" />
        <h3>Admin: Nytt Extratips</h3>
      </div>

      <form onSubmit={handleSubmit} className="grid md:grid-cols-2 gap-4">
        {/* Question */}
        <div className="md:col-span-2">
          <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Fråga *</label>
          <input
            value={formData.question}
            onChange={e => setFormData({ ...formData, question: e.target.value })}
            className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner"
            placeholder="Vem gör flest mål?"
            required
          />
        </div>

        {/* Desctiption */}
        <div className="md:col-span-2">
          <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Beskrivning</label>
          <input
            value={formData.description}
            onChange={e => setFormData({ ...formData, description: e.target.value })}
            className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner"
          />
        </div>

        {/* Points */}
        <div>
          <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Poäng *</label>
          <input
            type="number"
            value={formData.points}
            onChange={e => setFormData({ ...formData, points: parseInt(e.target.value) })}
            className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner"
            required
          />
        </div>

        {/* Deadline */}
        <div>
          <label className="block text-xs font-bold text-slate-400 uppercase mb-1 tracking-widest">Deadline *</label>
          <input
            type="datetime-local"
            value={formData.deadline}
            onChange={e => setFormData({ ...formData, deadline: e.target.value })}
            className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none text-slate-900 font-medium shadow-inner"
            required
          />
        </div>

        {/* Checkboxes */}
        <div className="md:col-span-2 bg-slate-50 p-4 rounded-xl border border-slate-200 shadow-inner space-y-2">
          {/* Allow custom input  */}
          <div className="flex items-center gap-2">
            <input
              type="checkbox"
              id="allowCustomTourney"
              checked={formData.allowCustom}
              onChange={e => setFormData({ ...formData, allowCustom: e.target.checked })}
              className="w-4 h-4 rounded border-slate-300 text-accent focus:ring-accent"
            />
            <label htmlFor="allowCustomTourney" className="text-xs font-black text-slate-500 uppercase tracking-widest">
              Tillåt fritext / egna värden
            </label>
          </div>

          {/* Provide predefined options */}
          <div className="flex items-center gap-2">
            <input
              type="checkbox"
              id="showOptionsInput"
              checked={showOptionsInput}
              onChange={e => setShowOptionsInput(e.target.checked)}
              className="w-4 h-4 rounded border-slate-300 text-accent focus:ring-accent"
            />
            <label htmlFor="showOptionsInput" className="text-xs font-black text-slate-500 uppercase tracking-widest">
              Ange förvalda alternativ
            </label>
          </div>

          {showOptionsInput && (
            <textarea
              value={formData.optionsString}
              onChange={e => setFormData({ ...formData, optionsString: e.target.value })}
              className="w-full px-4 py-2.5 bg-white border border-slate-200 rounded-xl h-20 outline-none text-slate-900 font-medium focus:ring-4 focus:ring-accent/10 focus:border-accent shadow-sm mt-2"
              placeholder="Mbappé, Kane, Isak..."
            />
          )}
        </div>

        {/* FormButtons */}
        <div className="md:col-span-2 mt-2">
          <ErrorMessage message={errorMessage} />
          <FormButtons
            isSubmitting={isLoading}
            showSuccess={createSuccess}
            onCancel={onCancel}
            isCreate={true}
            saveLabel="Skapa"
            successLabel="Sparat!"
            layout="row"
          />
        </div>
      </form>
    </div>
  );
};
