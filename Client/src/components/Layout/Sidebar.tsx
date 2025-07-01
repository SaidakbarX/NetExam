import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { motion } from 'framer-motion';
import {
  Home,
  BookOpen,
  Code,
  BarChart3,
  Users,
  Settings,
  FileText,
  Trophy,
  Clock
} from 'lucide-react';
import { useAuth } from '../../contexts/AuthContext';

const Sidebar: React.FC = () => {
  const location = useLocation();
  const { user } = useAuth();

  const menuItems = [
    { icon: Home, label: 'Dashboard', path: '/dashboard', roles: ['Student', 'Teacher', 'Admin'] },
    { icon: BookOpen, label: 'Exams', path: '/exams', roles: ['Student', 'Teacher', 'Admin'] },
    { icon: Code, label: 'Code Questions', path: '/questions', roles: ['Teacher', 'Admin'] },
    { icon: Trophy, label: 'Results', path: '/results', roles: ['Student', 'Teacher', 'Admin'] },
    { icon: BarChart3, label: 'Analytics', path: '/analytics', roles: ['Teacher', 'Admin'] },
    { icon: Users, label: 'Users', path: '/users', roles: ['Admin'] },
    { icon: Settings, label: 'Settings', path: '/settings', roles: ['Student', 'Teacher', 'Admin'] }
  ];

  const filteredMenuItems = menuItems.filter(item => 
    user && item.roles.includes(user.role)
  );

  return (
    <motion.aside
      initial={{ x: -250 }}
      animate={{ x: 0 }}
      className="fixed left-0 top-0 h-full w-64 bg-white border-r border-gray-200 shadow-lg z-40"
    >
      <div className="p-6">
        <div className="flex items-center space-x-3 mb-8">
          <div className="w-10 h-10 bg-gradient-to-r from-blue-500 to-purple-600 rounded-lg flex items-center justify-center">
            <Code className="w-6 h-6 text-white" />
          </div>
          <div>
            <h1 className="text-xl font-bold text-gray-900">ExamCode</h1>
            <p className="text-sm text-gray-500">Management System</p>
          </div>
        </div>

        <nav className="space-y-2">
          {filteredMenuItems.map((item) => {
            const isActive = location.pathname === item.path;
            return (
              <Link
                key={item.path}
                to={item.path}
                className={`flex items-center space-x-3 px-4 py-3 rounded-lg transition-all duration-200 group ${
                  isActive
                    ? 'bg-blue-50 text-blue-600 border-r-2 border-blue-600'
                    : 'text-gray-600 hover:bg-gray-50 hover:text-gray-900'
                }`}
              >
                <item.icon
                  className={`w-5 h-5 transition-colors ${
                    isActive ? 'text-blue-600' : 'text-gray-400 group-hover:text-gray-600'
                  }`}
                />
                <span className="font-medium">{item.label}</span>
              </Link>
            );
          })}
        </nav>

        {user && (
          <div className="absolute bottom-6 left-6 right-6">
            <div className="bg-gradient-to-r from-blue-50 to-purple-50 rounded-lg p-4 border border-blue-100">
              <div className="flex items-center space-x-3">
                <div className="w-10 h-10 bg-gradient-to-r from-blue-500 to-purple-600 rounded-full flex items-center justify-center text-white font-bold">
                  {user.email.charAt(0).toUpperCase()}
                </div>
                <div className="flex-1 min-w-0">
                  <p className="text-sm font-medium text-gray-900 truncate">
                    {user.email}
                  </p>
                  <p className="text-xs text-gray-500">{user.role}</p>
                </div>
              </div>
            </div>
          </div>
        )}
      </div>
    </motion.aside>
  );
};

export default Sidebar;