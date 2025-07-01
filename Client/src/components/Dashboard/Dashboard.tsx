import React, { useEffect, useState } from 'react';
import { motion } from 'framer-motion';
import {
  BookOpen,
  Code,
  Trophy,
  Clock,
  TrendingUp,
  Users,
  CheckCircle,
  AlertCircle
} from 'lucide-react';
import Header from '../Layout/Header';
import { apiService } from '../../services/api';
import { useAuth } from '../../contexts/AuthContext';

interface DashboardStats {
  activeExams: number;
  completedExams: number;
  totalQuestions: number;
  avgScore: number;
}

const Dashboard: React.FC = () => {
  const { user } = useAuth();
  const [stats, setStats] = useState<DashboardStats>({
    activeExams: 0,
    completedExams: 0,
    totalQuestions: 0,
    avgScore: 0
  });
  const [recentActivity, setRecentActivity] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadDashboardData();
  }, []);

  const loadDashboardData = async () => {
    try {
      setLoading(true);
      // Load various data based on user role
      const [exams, questions, results] = await Promise.all([
        apiService.getExams().catch(() => []),
        apiService.getCodeQuestions().catch(() => []),
        user ? apiService.getUserExamResults(user.id).catch(() => []) : Promise.resolve([])
      ]);

      setStats({
        activeExams: exams.filter((exam: any) => exam.isActive).length,
        completedExams: results.length,
        totalQuestions: questions.length,
        avgScore: results.length > 0 
          ? results.reduce((sum: number, result: any) => sum + (result.totalScore / result.maxScore * 100), 0) / results.length 
          : 0
      });

      setRecentActivity([
        ...results.slice(0, 5).map((result: any) => ({
          type: 'exam_completed',
          title: `Completed ${result.exam?.title || 'Exam'}`,
          time: result.completedAt,
          score: Math.round(result.totalScore / result.maxScore * 100)
        }))
      ]);
    } catch (error) {
      console.error('Error loading dashboard data:', error);
    } finally {
      setLoading(false);
    }
  };

  const StatCard = ({ icon: Icon, title, value, change, color }: any) => (
    <motion.div
      whileHover={{ y: -5 }}
      className="bg-white rounded-xl p-6 shadow-sm border border-gray-100 hover:shadow-md transition-all duration-200"
    >
      <div className="flex items-center justify-between">
        <div>
          <p className="text-sm font-medium text-gray-600">{title}</p>
          <p className="text-2xl font-bold text-gray-900 mt-1">{value}</p>
          {change && (
            <p className={`text-sm mt-1 flex items-center ${change > 0 ? 'text-green-600' : 'text-red-600'}`}>
              <TrendingUp className="w-4 h-4 mr-1" />
              {change > 0 ? '+' : ''}{change}%
            </p>
          )}
        </div>
        <div className={`w-12 h-12 rounded-lg ${color} flex items-center justify-center`}>
          <Icon className="w-6 h-6 text-white" />
        </div>
      </div>
    </motion.div>
  );

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50">
        <Header title="Dashboard" subtitle="Welcome back! Here's your overview" />
        <div className="ml-64 p-6">
          <div className="animate-pulse space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
              {[1, 2, 3, 4].map((i) => (
                <div key={i} className="bg-gray-200 h-32 rounded-xl"></div>
              ))}
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <Header title="Dashboard" subtitle={`Welcome back, ${user?.email}!`} />
      
      <div className="ml-64 p-6">
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          className="space-y-6"
        >
          {/* Stats Grid */}
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            <StatCard
              icon={BookOpen}
              title="Active Exams"
              value={stats.activeExams}
              change={12}
              color="bg-gradient-to-r from-blue-500 to-blue-600"
            />
            <StatCard
              icon={CheckCircle}
              title="Completed"
              value={stats.completedExams}
              change={8}
              color="bg-gradient-to-r from-green-500 to-green-600"
            />
            <StatCard
              icon={Code}
              title="Questions"
              value={stats.totalQuestions}
              change={-3}
              color="bg-gradient-to-r from-purple-500 to-purple-600"
            />
            <StatCard
              icon={Trophy}
              title="Avg Score"
              value={`${Math.round(stats.avgScore)}%`}
              change={5}
              color="bg-gradient-to-r from-amber-500 to-amber-600"
            />
          </div>

          {/* Recent Activity */}
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
            <motion.div
              initial={{ opacity: 0, x: -20 }}
              animate={{ opacity: 1, x: 0 }}
              className="bg-white rounded-xl p-6 shadow-sm border border-gray-100"
            >
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Recent Activity</h3>
              <div className="space-y-4">
                {recentActivity.length > 0 ? recentActivity.map((activity, index) => (
                  <div key={index} className="flex items-center space-x-3 p-3 bg-gray-50 rounded-lg">
                    <div className="w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center">
                      <Trophy className="w-4 h-4 text-blue-600" />
                    </div>
                    <div className="flex-1">
                      <p className="text-sm font-medium text-gray-900">{activity.title}</p>
                      <p className="text-xs text-gray-500">{new Date(activity.time).toLocaleDateString()}</p>
                    </div>
                    {activity.score && (
                      <span className="text-sm font-semibold text-green-600">{activity.score}%</span>
                    )}
                  </div>
                )) : (
                  <p className="text-gray-500 text-center py-4">No recent activity</p>
                )}
              </div>
            </motion.div>

            <motion.div
              initial={{ opacity: 0, x: 20 }}
              animate={{ opacity: 1, x: 0 }}
              className="bg-white rounded-xl p-6 shadow-sm border border-gray-100"
            >
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Quick Actions</h3>
              <div className="space-y-3">
                <motion.button
                  whileHover={{ scale: 1.02 }}
                  className="w-full text-left p-4 bg-gradient-to-r from-blue-50 to-purple-50 rounded-lg border border-blue-100 hover:border-blue-200 transition-all"
                >
                  <div className="flex items-center space-x-3">
                    <BookOpen className="w-5 h-5 text-blue-600" />
                    <div>
                      <p className="font-medium text-gray-900">Take an Exam</p>
                      <p className="text-sm text-gray-600">Start a new exam session</p>
                    </div>
                  </div>
                </motion.button>

                <motion.button
                  whileHover={{ scale: 1.02 }}
                  className="w-full text-left p-4 bg-gradient-to-r from-green-50 to-emerald-50 rounded-lg border border-green-100 hover:border-green-200 transition-all"
                >
                  <div className="flex items-center space-x-3">
                    <Code className="w-5 h-5 text-green-600" />
                    <div>
                      <p className="font-medium text-gray-900">Practice Problems</p>
                      <p className="text-sm text-gray-600">Solve coding challenges</p>
                    </div>
                  </div>
                </motion.button>

                <motion.button
                  whileHover={{ scale: 1.02 }}
                  className="w-full text-left p-4 bg-gradient-to-r from-purple-50 to-pink-50 rounded-lg border border-purple-100 hover:border-purple-200 transition-all"
                >
                  <div className="flex items-center space-x-3">
                    <Trophy className="w-5 h-5 text-purple-600" />
                    <div>
                      <p className="font-medium text-gray-900">View Results</p>
                      <p className="text-sm text-gray-600">Check your performance</p>
                    </div>
                  </div>
                </motion.button>
              </div>
            </motion.div>
          </div>
        </motion.div>
      </div>
    </div>
  );
};

export default Dashboard;