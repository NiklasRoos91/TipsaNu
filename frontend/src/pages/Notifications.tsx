import { useNotifications } from '../hooks/useNotifications';
import { NotificationsList } from '../components/notifications/NotificationsList';
import { Bell } from 'lucide-react';

export const Notifications = () => {
  const { notifications, loading, error } = useNotifications();

  return (
    <div className="max-w-2xl mx-auto pb-12">
      <div className="flex items-center justify-between mb-8">
        <div>
          <h2 className="text-3xl font-bold text-primary flex items-center gap-3">
            <Bell className="text-accent" />
            Notiser
          </h2>
          <p className="text-slate-500 mt-1 font-medium">Håll koll på dina tips och ligor</p>
        </div>
        {notifications.length > 0 && !loading && (
          <span className="bg-slate-100 text-slate-500 px-3 py-1 rounded-full text-xs font-bold border border-slate-200">
            {notifications.length} totalt
          </span>
        )}
      </div>

      {error ? (
        <div className="bg-red-50 text-red-600 p-6 rounded-2xl border border-red-100 text-center font-bold">
          {error}
        </div>
      ) : (
        <NotificationsList 
          notifications={notifications} 
          loading={loading} 
        />
      )}
    </div>
  );
};