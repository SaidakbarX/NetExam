import React, { useEffect, useState } from 'react';
import { motion } from 'framer-motion';
import { Clock, Users, PlayCircle, Calendar, Plus, Edit, Trash2 } from 'lucide-react';
import Header from '../Layout/Header';
import { apiService } from '../../services/api';
import { useAuth } from '../../contexts/AuthContext';
import { Exam } from '../../types/api';

const ExamList: React.FC = () => {
  const { user } = useAuth();
  const [exams, setExams] = useState<Exam[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadExams();
  }, []);

  const loadExams = async () => {
    try {
      setLoading(true);
      const data = await apiService.getExams();
      setExams(data);
    } catch (error) {
      console.error('Error loading exams:', error);
    } finally {
      setLoading(false);
    }
  };

  const formatDuration = (minutes: number) => {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return hours > 0 ? `${hours}h ${mins}m` : `${mins}m`;
  };

  const isExamActive = (exam: Exam) => {
    const now = new Date();
    const startTime = new Date(exam.startTime);
    const endTime = new Date(exam.endTime);
    return now >= startTime && now <= endTime && exam.isActive;
  };

  const ExamCard = ({ exam }: { exam: Exam }) => (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      whileHover={{ y: -5 }}
      className="bg-white rounded-xl p-6 shadow-sm border border-gray-100 hover:shadow-md transition-all duration-200"
    >
      <div className="flex items-start justify-between mb-4">
        <div className="flex-1">
          <h3 className="text-lg font-semibold text-gray-900 mb-2">{exam.title}</h3>
          <p className="text-gray-600 text-sm mb-3">{exam.description}</p>
        </div>
        <div className={`px-3 py-1 rounded-full text-xs font-medium ${
          isExamActive(exam)
            ? 'bg-green-100 text-green-800'
            : exam.isActive
            ? 'bg-blue-100 text-blue-800'
            : 'bg-gray-100 text-gray-800'
        }`}>
          {isExamActive(exam) ? 'Active' : exam.isActive ? 'Scheduled' : 'Inactive'}
        </div>
      </div>

      <div className="grid grid-cols-2 gap-4 mb-4">
        <div className="flex items-center text-sm text-gray-600">
          <Clock className="w-4 h-4 mr-2" />
          {formatDuration(exam.duration)}
        </div>
        <div className="flex items-center text-sm text-gray-600">
          <Calendar className="w-4 h-4 mr-2" />
          {new Date(exam.startTime).toLocaleDateString()}
        </div>
      </div>

      <div className="flex items-center justify-between">
        <div className="text-sm text-gray-500">
          Created {new Date(exam.createdAt).toLocaleDateString()}
        </div>
        <div className="flex space-x-2">
          {isExamActive(exam) && user?.role === 'Student' && (
            <motion.button
              whileHover={{ scale: 1.05 }}
              whileTap={{ scale: 0.95 }}
              className="px-4 py-2 bg-gradient-to-r from-green-500 to-green-600 text-white rounded-lg text-sm font-medium hover:from-green-600 hover:to-green-700 transition-all flex items-center"
            >
              <PlayCircle className="w-4 h-4 mr-1" />
              Start Exam
            </motion.button>
          )}
          {(user?.role === 'Teacher' || user?.role === 'Admin') && (
            <>
              <motion.button
                whileHover={{ scale: 1.05 }}
                className="p-2 text-blue-600 hover:bg-blue-50 rounded-lg transition-all"
              >
                <Edit className="w-4 h-4" />
              </motion.button>
              <motion.button
                whileHover={{ scale: 1.05 }}
                className="p-2 text-red-600 hover:bg-red-50 rounded-lg transition-all"
              >
                <Trash2 className="w-4 h-4" />
              </motion.button>
            </>
          )}
        </div>
      </div>
    </motion.div>
  );

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50">
        <Header title="Exams" subtitle="Manage and take your exams" />
        <div className="ml-64 p-6">
          <div className="animate-pulse grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {[1, 2, 3, 4, 5, 6].map((i) => (
              <div key={i} className="bg-gray-200 h-48 rounded-xl"></div>
            ))}
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <Header title="Exams" subtitle="Manage and take your exams" />
      
      <div className="ml-64 p-6">
        <div className="flex justify-between items-center mb-6">
          <div>
            <h2 className="text-xl font-semibold text-gray-900">
              {user?.role === 'Student' ? 'Available Exams' : 'All Exams'}
            </h2>
            <p className="text-gray-600">
              {exams.length} exam{exams.length !== 1 ? 's' : ''} found
            </p>
          </div>
          
          {(user?.role === 'Teacher' || user?.role === 'Admin') && (
            <motion.button
              whileHover={{ scale: 1.05 }}
              whileTap={{ scale: 0.95 }}
              className="px-4 py-2 bg-gradient-to-r from-blue-500 to-purple-600 text-white rounded-lg font-medium hover:from-blue-600 hover:to-purple-700 transition-all flex items-center"
            >
              <Plus className="w-4 h-4 mr-2" />
              Create Exam
            </motion.button>
          )}
        </div>

        {exams.length > 0 ? (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {exams.map((exam) => (
              <ExamCard key={exam.id} exam={exam} />
            ))}
          </div>
        ) : (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            className="text-center py-12"
          >
            <Calendar className="w-16 h-16 text-gray-300 mx-auto mb-4" />
            <h3 className="text-lg font-medium text-gray-900 mb-2">No exams available</h3>
            <p className="text-gray-600">
              {user?.role === 'Student' 
                ? 'There are no active exams at the moment.'
                : 'Get started by creating your first exam.'
              }
            </p>
          </motion.div>
        )}
      </div>
    </div>
  );
};

export default ExamList;