//window.centerLastRowItems = function (grid) {
//    if (grid && grid.children) {
//        const items = Array.from(grid.children);

//        // Remove the class from all items first.
//        items.forEach(item => item.classList.remove('last-row'));

//        const rows = getComputedStyle(grid).getPropertyValue('grid-template-rows').split(' ');
//        const lastRowItems = items.slice(-rows.length);

//        // Add the class to the items in the last row.
//        lastRowItems.forEach(item => item.classList.add('last-row'));
//    }
//    else {
//        console.error('grid is null here or grid.children is null');
//    }
//}
