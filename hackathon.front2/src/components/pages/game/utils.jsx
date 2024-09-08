export const trackUserVisit = () => {
    const streaks = JSON.parse(localStorage.getItem('streaks')) || { count: 0, lastVisit: null };
    const today = new Date().toISOString().split('T')[0];
  
    if (streaks.lastVisit !== today) {
      const lastVisitDate = new Date(streaks.lastVisit);
      const currentDate = new Date(today);
  

      if (
        !isNaN(lastVisitDate) &&
        currentDate.getTime() - lastVisitDate.getTime() === 86400000
      ) {
        streaks.count += 1;
      } 

      else {
        streaks.count = 1;
      }
  
      streaks.lastVisit = today;
      localStorage.setItem('streaks', JSON.stringify(streaks));
    }
  
    return streaks.count;
  };



  