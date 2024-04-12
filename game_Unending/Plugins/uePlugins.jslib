
mergeInto(LibraryManager.library, 
{
     IsMobile: function()
   {
      return (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent));
   }
} 
);  
