export const DataTableConfig = {
  pagingType: 'full_numbers',   // pagination style
  pageLength: 10,               // default rows per page
  processing: true,             // show "processing" indicator
  responsive: true,             // auto-fit for mobile/tablet
  searching: true,              // enable search box
  ordering: true,               // enable column sorting
  info: true,                   // show "Showing X of Y entries"
  lengthChange: true,           // allow user to change page length
  autoWidth: false,             // prevent auto column width
  language: {                   // customize text labels
    search: "Filter records:",
    lengthMenu: "Show _MENU_ entries",
    info: "Showing _START_ to _END_ of _TOTAL_ entries",
    infoEmpty: "No records available",
    paginate: {
      first: "First",
      last: "Last",
      next: "Next",
      previous: "Previous"
    }
  }
};
