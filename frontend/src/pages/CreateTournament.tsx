
import React, { useState } from 'react';
import { Shield } from 'lucide-react';
import { CreateTournamentForm } from '../components/tournament/CreateTournamentForm';
import { TournamentFormData } from '../hooks/useTournamentForm';

// ⚠️ Temporary placeholder to avoid TS error, replace with real API call later
const createTournament = async (data: TournamentFormData): Promise<any> => {
  console.log('Mock createTournament called with:', data);
  return new Promise((resolve) => setTimeout(() => resolve(true), 500));
};


interface CreateTournamentProps {
  onCreated?: () => void;
}

export const CreateTournament: React.FC<CreateTournamentProps> = ({ onCreated }) => {
  // Local state only for the preview
  const [previewData, setPreviewData] = useState<TournamentFormData>({
    name: '',
    startDate: '',
    endDate: '',
    bannerUrl: 'https://images.unsplash.com/photo-1508098682722-e99c43a406b2?q=80&w=1200&auto=format&fit=crop'
  });

  const handleFormSubmit = async (data: TournamentFormData): Promise<boolean> => {
    try {
      await createTournament({
        name: data.name,
        startDate: new Date(data.startDate).toISOString(),
        endDate: new Date(data.endDate).toISOString(),
        bannerUrl: data.bannerUrl
      });
      
      if (onCreated) {
        setTimeout(onCreated, 2000);
      }
      return true;
    } catch (err) {
      alert('Kunde inte skapa turnering.');
      return false;
    }
  };

  const handleCancel = () => {
    if (onCreated) onCreated();
  };

  return (
    <div className="max-w-4xl mx-auto">
      {/* Header Info */}
      <div className="flex items-center gap-2 text-primary font-bold mb-4 px-1">
        <Shield size={20} className="text-purple-600" />
        <h3 className="text-lg">Admin: Ny turnering</h3>
      </div>

      {/* Modular Form Component */}
      <div className="mb-10">
        <CreateTournamentForm 
          onSubmit={handleFormSubmit}
          onCancel={handleCancel}
          onFormChange={setPreviewData}
        />
      </div>

      {/* Preview Section */}
      <div className="px-2">
        <h3 className="text-[10px] font-black text-slate-400 uppercase tracking-[0.2em] mb-4">Förhandsvisning av kort</h3>
        <div className="relative h-44 rounded-3xl overflow-hidden shadow-xl border border-white">
          <img 
            src={previewData.bannerUrl || 'https://images.unsplash.com/photo-1508098682722-e99c43a406b2'} 
            alt="Preview" 
            className="w-full h-full object-cover transition-all" 
          />
          <div className="absolute inset-0 bg-gradient-to-t from-slate-900/90 via-slate-900/30 to-transparent flex items-end p-6">
            <div>
               <div className="bg-accent text-white text-[10px] font-black px-2 py-0.5 rounded-full inline-block mb-1 shadow-sm uppercase tracking-wider">
                  KOMMANDE
               </div>
               <h4 className="text-xl font-bold text-white tracking-tight">{previewData.name || 'Namn på turnering'}</h4>
               <p className="text-slate-300 text-xs mt-1">
                 {previewData.startDate ? new Date(previewData.startDate).toLocaleDateString('sv-SE') : 'Startdatum'} - {previewData.endDate ? new Date(previewData.endDate).toLocaleDateString('sv-SE') : 'Slutdatum'}
               </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
