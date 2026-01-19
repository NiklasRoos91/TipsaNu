import React from 'react';
import { Notification } from '../../types/types';
import { NotificationItem } from './NotificationItem';
import { EmptyState } from './EmptyState';

interface NotificationsListProps {
  notifications: Notification[];
  loading: boolean;
}

export const NotificationsList: React.FC<NotificationsListProps> = ({ notifications, loading }) => {
  if (loading) {
    return (
      <div className="bg-white rounded-2xl shadow-sm border border-slate-200 overflow-hidden divide-y divide-slate-100">
        {[...Array(3)].map((_, i) => (
          <div key={i} className="p-5 animate-pulse flex gap-4">
            <div className="w-2.5 h-2.5 bg-slate-200 rounded-full mt-1.5" />
            <div className="flex-1 space-y-3">
              <div className="flex justify-between">
                <div className="h-4 bg-slate-100 rounded w-1/3" />
                <div className="h-3 bg-slate-50 rounded w-1/5" />
              </div>
              <div className="h-3 bg-slate-100 rounded w-3/4" />
            </div>
          </div>
        ))}
      </div>
    );
  }

  if (notifications.length === 0) {
    return <EmptyState />;
  }

  return (
    <div className="bg-white rounded-2xl shadow-sm border border-slate-200 overflow-hidden animate-fade-in">
      {notifications.map((n) => (
        <NotificationItem key={n.id} notification={n} />
      ))}
    </div>
  );
};