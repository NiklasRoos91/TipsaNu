import React from 'react';
import { Link } from 'react-router-dom';

// Placeholder type for notification, replace with actual type from your data model
type Notification = {
  id: string;
  title: string;
  message: string;
  link?: string;
  read: boolean;
  createdAt: string;
};

interface NotificationItemProps {
  notification: Notification;
}

export const NotificationItem: React.FC<NotificationItemProps> = ({ notification }) => {
  const dateStr = new Date(notification.createdAt).toLocaleDateString('sv-SE', {
    day: 'numeric',
    month: 'short',
    hour: '2-digit',
    minute: '2-digit',
  });

  return (
    <Link 
      to={notification.link || '#'} 
      className="block hover:bg-slate-50 transition-colors p-5 border-b border-slate-100 last:border-0"
    >
      <div className="flex gap-4">
        <div className={`w-2.5 h-2.5 rounded-full mt-1.5 flex-shrink-0 transition-all ${
          notification.read ? 'bg-slate-200' : 'bg-accent shadow-[0_0_8px_rgba(16,185,129,0.5)]'
        }`} />
        <div className="flex-1">
          <div className="flex justify-between items-start gap-4">
            <h4 className={`font-bold leading-tight ${notification.read ? 'text-slate-600' : 'text-primary'}`}>
              {notification.title}
            </h4>
            <span className="text-[10px] font-bold text-slate-400 uppercase tracking-wider whitespace-nowrap">
              {dateStr}
            </span>
          </div>
          <p className="text-slate-500 text-sm mt-1 leading-relaxed">
            {notification.message}
          </p>
        </div>
      </div>
    </Link>
  );
};