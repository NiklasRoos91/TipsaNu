
import React, { useState } from 'react';
import { useAuth } from '../hooks/useAuth';
import { Mail, Info, Camera } from 'lucide-react';

export const UserProfile = () => {
  const { user, updateUser } = useAuth();
  const [bio, setBio] = useState(user?.bio || '');
  const [isSaving, setIsSaving] = useState(false);

  const handleSave = async () => {
    if (!user) return;
    setIsSaving(true);
    try {
      await updateUser({ bio });
      alert('Profil uppdaterad!');
    } catch (e) {
      alert('Kunde inte spara profil.');
    } finally {
      setIsSaving(false);
    }
  };

  if (!user) return null;

  return (
    <div className="max-w-2xl mx-auto animate-fade-in">
      <div className="mb-8">
        <h2 className="text-3xl font-bold text-primary">Min Profil</h2>
        <p className="text-slate-500 mt-1">Hantera dina uppgifter och se din statistik.</p>
      </div>

      <div className="bg-white rounded-2xl shadow-sm border border-slate-200 p-8 space-y-8">
        {/* User Identity Header */}
        <div className="flex items-center gap-6 pb-8 border-b border-slate-100">
          <div className="relative group">
            <img 
              src={user.avatarUrl || `https://ui-avatars.com/api/?name=${user.displayName}&background=random`} 
              className="w-24 h-24 rounded-2xl border-2 border-slate-100 bg-slate-50 object-cover shadow-sm" 
              alt="Avatar" 
            />
            <button className="absolute inset-0 bg-black/20 rounded-2xl flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity">
              <Camera className="text-white" size={20} />
            </button>
          </div>
          <div>
            <h3 className="text-2xl font-bold text-primary">{user.displayName}</h3>
            <p className="text-slate-500 font-medium">@{user.username}</p>
          </div>
        </div>

        <div className="grid gap-6">
          {/* Email field (Read-only) */}
          <div>
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-2">E-post</label>
            <div className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-3 text-slate-500 font-medium flex items-center gap-3">
              <Mail size={16} className="text-slate-400" />
              {user.email}
            </div>
            <p className="text-[10px] text-slate-400 mt-2 italic font-medium">E-postadressen kan inte ändras för tillfället.</p>
          </div>

          {/* Bio field (Editable) */}
          <div>
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-2">Om mig</label>
            <div className="relative">
              <Info size={16} className="absolute left-4 top-4 text-slate-400" />
              <textarea 
                value={bio}
                onChange={(e) => setBio(e.target.value)}
                className="w-full bg-slate-50 border border-slate-200 rounded-xl pl-12 pr-4 py-3 h-32 focus:ring-2 focus:ring-accent focus:border-accent outline-none transition-all text-slate-900 font-medium resize-none"
                placeholder="Berätta lite om dig själv..."
              />
            </div>
          </div>
        </div>

        {/* Action Buttons */}
        <div className="flex justify-end pt-4 border-t border-slate-100">
          <button 
            onClick={handleSave}
            disabled={isSaving}
            className="bg-accent hover:bg-emerald-600 text-white px-10 py-3 rounded-xl font-bold transition-all shadow-lg active:scale-95 disabled:opacity-50 text-sm uppercase tracking-widest"
          >
            {isSaving ? 'Sparar...' : 'Spara ändringar'}
          </button>
        </div>
      </div>
    </div>
  );
};
