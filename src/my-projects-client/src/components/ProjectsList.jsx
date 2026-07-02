import React, { useState, useEffect } from 'react';

function ProjectsList() {
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Робимо запит до вашого .NET API на порт 5191
    fetch('http://localhost:5191/api/projects')
      .then((response) => {
        if (!response.ok) {
          throw new Error(`Помилка сервера: ${response.status}`);
        }
        return response.json();
      })
      .then((data) => {
        setProjects(data);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  if (loading) return <div style={styles.center}>Завантаження проектів...</div>;
  if (error) return <div style={{ ...styles.center, color: 'red' }}>Помилка: {error}</div>;

  return (
    <div style={styles.container}>
      <h1 style={styles.title}>Мої Проекти</h1>
      
      {projects.length === 0 ? (
        <p style={styles.empty}>У базі даних ще немає жодного проекту. База порожня!</p>
      ) : (
        <div style={styles.list}>
          {projects.map((project) => (
            <div key={project.id} style={styles.card}>
              <h2 style={styles.projectTitle}>{project.title}</h2>
              <p style={styles.projectDesc}>{project.description}</p>
              <div style={styles.techStack}>
                <strong>Стек:</strong> {project.technologyStack}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

// Прості стилі для гарного вигляду
const styles = {
  container: { maxWidth: '800px', margin: '0 auto', fontFamily: 'Arial, sans-serif' },
  title: { textAlign: 'center', color: '#333', marginBottom: '30px' },
  center: { textAlign: 'center', marginTop: '50px', fontSize: '18px' },
  empty: { textAlign: 'center', color: '#666', fontStyle: 'italic' },
  list: { display: 'flex', flexDirection: 'column', gap: '20px' },
  card: { backgroundColor: '#fff', padding: '20px', borderRadius: '8px', boxShadow: '0 4px 6px rgba(0,0,0,0.1)' },
  projectTitle: { margin: '0 0 10px 0', color: '#0070f3' },
  projectDesc: { color: '#555', lineHeight: '1.5' },
  techStack: { marginTop: '15px', fontSize: '14px', color: '#444', borderTop: '1px solid #eee', paddingTop: '10px' }
};

export default ProjectsList;