// StreakComponent.js
import { useEffect, useState } from 'react';
import { trackUserVisit } from './utils';

const StreakComponent = () => {
  const [streakCount, setStreakCount] = useState(0);

  useEffect(() => {
    const streaks = trackUserVisit();
    setStreakCount(streaks);
    

    console.log(`У вас ${streaks} стриков!`);
  }, []);

  return (
    <div>
      {streakCount > 0 && (
        <div>
          
          <img src="brain-icon.png" alt="Streak Icon" />
          <p>Congrats! You’ve earned {streakCount} streak(s)!</p>
        </div>
      )}
      
    </div>
  );
  
};

export default StreakComponent;
