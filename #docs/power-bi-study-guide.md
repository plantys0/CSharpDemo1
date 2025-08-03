# Power BI Developer Interview Study Guide

## Overview
This comprehensive study guide covers essential Power BI concepts for your interview with Infosys. Focus on practical applications and real-world scenarios.

## 50 Essential Power BI Interview Questions & Answers

### Basic Concepts (Questions 1-15)

**1. What is Power BI and its main components?**
Power BI is Microsoft's business intelligence platform consisting of:
- **Power BI Desktop**: Development tool for creating reports
- **Power BI Service**: Cloud-based platform for sharing reports
- **Power BI Mobile**: Mobile app for viewing reports
- **Power BI Gateway**: Connects on-premises data to cloud

**2. What is the difference between Power BI Desktop and Power BI Service?**
- **Desktop**: Local application for creating reports, free to use
- **Service**: Cloud platform for sharing, collaboration, requires license

**3. Explain Power Query and its purpose.**
Power Query is the ETL tool in Power BI for:
- Data extraction from multiple sources
- Data transformation and cleaning
- Data shaping and preparation
- Uses M language for transformations

**4. What is DAX and why is it important?**
DAX (Data Analysis Expressions) is a formula language for:
- Creating calculated columns and measures
- Performing complex calculations
- Time intelligence functions
- Advanced analytics

**5. Difference between calculated columns and measures?**
- **Calculated Columns**: Computed at data refresh, stored in model, row-level
- **Measures**: Computed at query time, aggregated values, dynamic

**6. What are the different data connection modes in Power BI?**
- **Import**: Data stored in Power BI model, fastest performance
- **DirectQuery**: Real-time connection to source, slower performance
- **Live Connection**: Direct connection to Analysis Services/Power BI datasets

**7. What is a data model in Power BI?**
A data model defines:
- Relationships between tables
- Calculated columns and measures
- Hierarchies and data types
- Security rules

**8. Explain star schema vs snowflake schema.**
- **Star Schema**: Fact table surrounded by dimension tables, denormalized
- **Snowflake Schema**: Normalized dimension tables, more complex joins

**9. What are Power BI filters and their types?**
- **Visual-level filters**: Apply to specific visuals
- **Page-level filters**: Apply to entire page
- **Report-level filters**: Apply to entire report
- **Drillthrough filters**: Navigate between report pages

**10. What is the difference between a report and a dashboard?**
- **Report**: Multi-page detailed analysis, created in Desktop
- **Dashboard**: Single-page summary, created in Service, tiles from reports

**11. How do you refresh data in Power BI?**
- **Manual refresh**: On-demand refresh in Desktop/Service
- **Scheduled refresh**: Automated refresh in Service
- **Gateway refresh**: For on-premises data sources

**12. What are Power BI workspaces?**
Workspaces are containers for:
- Reports and dashboards
- Datasets and dataflows
- Collaboration spaces for teams
- Access control and permissions

**13. Explain row-level security (RLS) in Power BI.**
RLS restricts data access based on:
- User roles and filters
- DAX expressions for security
- Dynamic security using USERNAME() function

**14. What are Power BI templates?**
Templates (.pbit files) contain:
- Report structure without data
- Queries and transformations
- Visualizations and formatting
- Can be shared and reused

**15. What is Power BI Gateway and its types?**
Gateway connects on-premises data to cloud:
- **Personal Gateway**: Individual use, import mode only
- **On-premises Gateway**: Enterprise use, supports DirectQuery

### Data Modeling (Questions 16-25)

**16. What are relationships in Power BI and their types?**
Relationships connect tables:
- **One-to-Many**: Most common, dimension to fact
- **One-to-One**: Rare, typically split tables
- **Many-to-Many**: Complex, requires bridge tables

**17. What is cross-filtering and its directions?**
Cross-filtering propagates filters between tables:
- **Single direction**: Filter flows one way
- **Bidirectional**: Filter flows both ways (use carefully)

**18. What are inactive relationships?**
Inactive relationships:
- Additional relationships between same tables
- Activated using USERELATIONSHIP() function
- Useful for role-playing dimensions

**19. How do you handle many-to-many relationships?**
- Create bridge table with unique combinations
- Use SUMMARIZE() or CROSSJOIN() functions
- Enable bidirectional filtering cautiously

**20. What is a role-playing dimension?**
Single dimension table serving multiple purposes:
- Date table for Order Date, Ship Date, Due Date
- Use inactive relationships and USERELATIONSHIP()

**21. What are hierarchies in Power BI?**
Hierarchies enable drill-down capabilities:
- Natural hierarchies (Year > Quarter > Month)
- Created in model view
- Enable drill-down in visuals

**22. What is data lineage in Power BI?**
Data lineage shows:
- Data flow from source to reports
- Dependencies between datasets
- Impact analysis for changes

**23. What are composite models in Power BI?**
Composite models combine:
- Import and DirectQuery data
- Multiple data sources in one model
- Local calculations on DirectQuery data

**24. What is aggregation in Power BI?**
Aggregations improve performance by:
- Pre-calculating summary data
- Automatically routing queries
- Reducing data source load

**25. What are calculated tables in Power BI?**
Calculated tables are:
- Created using DAX expressions
- Useful for date tables, parameter tables
- Computed at refresh time

### DAX Functions (Questions 26-35)

**26. What are the main DAX function categories?**
- **Aggregation**: SUM, AVERAGE, COUNT, MAX, MIN
- **Filter**: FILTER, ALL, CALCULATE, RELATED
- **Time Intelligence**: DATESYTD, SAMEPERIODLASTYEAR
- **Logical**: IF, AND, OR, NOT
- **Text**: CONCATENATE, LEFT, RIGHT, FIND

**27. What is the difference between SUM and SUMX?**
- **SUM**: Aggregates a column directly
- **SUMX**: Iterates through table rows, evaluates expression

**28. Explain CALCULATE function and its importance.**
CALCULATE modifies filter context:
- Changes how measures are calculated
- Adds, removes, or modifies filters
- Most important DAX function

**29. What is filter context in DAX?**
Filter context determines:
- Which rows are visible to calculations
- Created by slicers, filters, row/column fields
- Modified by CALCULATE function

**30. What is row context in DAX?**
Row context:
- Current row being processed
- Exists in calculated columns
- Created by iterating functions (SUMX, FILTER)

**31. What are time intelligence functions?**
Functions for date-based calculations:
- **DATESYTD**: Year-to-date calculations
- **SAMEPERIODLASTYEAR**: Prior year comparisons
- **DATEADD**: Date arithmetic
- Requires proper date table

**32. What is the RELATED function?**
RELATED function:
- Retrieves values from related tables
- Follows relationships in model
- Used in calculated columns

**33. What is the FILTER function?**
FILTER function:
- Returns filtered table
- Used with aggregation functions
- More flexible than basic filters

**34. What are variables in DAX?**
Variables (VAR):
- Store intermediate calculations
- Improve performance and readability
- Defined with VAR, used with RETURN

**35. What is the ALL function?**
ALL function:
- Removes filters from columns/tables
- Returns all rows ignoring filters
- Used with CALCULATE for totals

### Visualizations (Questions 36-45)

**36. What are the different types of Power BI visuals?**
- **Charts**: Bar, column, line, pie, scatter
- **Tables**: Matrix, table, card
- **Maps**: Map, filled map, shape map
- **Specialized**: Gauge, KPI, funnel, treemap

**37. What is the difference between table and matrix visuals?**
- **Table**: Shows detailed data, flat structure
- **Matrix**: Shows summarized data, pivot table structure

**38. What are custom visuals in Power BI?**
Custom visuals:
- Third-party or custom-built visuals
- Available from AppSource marketplace
- Created using Power BI visuals SDK

**39. What is drill-down and drill-through?**
- **Drill-down**: Navigate hierarchy levels in same visual
- **Drill-through**: Navigate to another report page with filters

**40. What are bookmarks in Power BI?**
Bookmarks capture:
- Current state of report page
- Filters, slicers, and visual selections
- Used for storytelling and navigation

**41. What is conditional formatting in Power BI?**
Conditional formatting:
- Changes visual appearance based on values
- Background color, font color, data bars
- Based on rules or field values

**42. What are slicers in Power BI?**
Slicers are:
- Visual filters for user interaction
- Can be list, dropdown, or button style
- Synchronize across multiple pages

**43. What is the Q&A feature in Power BI?**
Q&A allows:
- Natural language queries
- Automatic visual generation
- Powered by AI and data modeling

**44. What are Power BI themes?**
Themes provide:
- Consistent visual formatting
- Color schemes and fonts
- JSON-based customization

**45. What is the decomposition tree visual?**
Decomposition tree:
- AI-powered visual for root cause analysis
- Automatic discovery of dimensions
- Drill-down into data hierarchies

### Advanced Topics (Questions 46-50)

**46. What is Power BI Premium?**
Premium provides:
- Dedicated capacity for better performance
- Larger dataset sizes
- Advanced features like paginated reports
- AI capabilities

**47. What are paginated reports in Power BI?**
Paginated reports:
- Pixel-perfect formatted reports
- Optimized for printing
- Created with Power BI Report Builder

**48. What is Power BI Embedded?**
Embedded allows:
- Integration of Power BI into custom applications
- White-label analytics solutions
- Programmatic access via APIs

**49. What is dataflows in Power BI?**
Dataflows provide:
- Data preparation in the cloud
- Reusable data transformations
- Common data model integration

**50. What are Power BI APIs?**
APIs enable:
- Programmatic access to Power BI
- Automation of administrative tasks
- Integration with other systems

## Key Cheat Sheets and Resources

### Essential DAX Functions Cheat Sheet
```
Common Functions:
- SUM(Table[Column])
- CALCULATE(measure, filter1, filter2)
- RELATED(Table[Column])
- FILTER(Table, condition)
- ALL(Table/Column)
- SAMEPERIODLASTYEAR(dates)
- DATESYTD(dates)
```

### Quick Reference Links
- **Power BI Documentation**: https://docs.microsoft.com/en-us/power-bi/
- **DAX Reference**: https://docs.microsoft.com/en-us/dax/
- **Power BI Community**: https://community.powerbi.com/
- **Guy in a Cube YouTube**: https://www.youtube.com/c/GuyInACube

### YouTube Video Recommendations
1. **Power BI Tutorial for Beginners** (Guy in a Cube)
2. **Power BI Full Course Tutorial** (Learnit Training)
3. **DAX Fundamentals** (SQLBI)
4. **Power BI Performance Optimization** (Various channels)

### Study Strategy for Next 5 Hours
1. **Hour 1**: Review basic concepts and data connections
2. **Hour 2**: Practice DAX functions and calculations
3. **Hour 3**: Focus on data modeling and relationships
4. **Hour 4**: Understand visualizations and report design
5. **Hour 5**: Review advanced topics and practice scenarios

### Common Interview Scenarios
1. **Performance Optimization**: How to optimize slow reports
2. **Data Modeling**: Design efficient star schema
3. **Security**: Implement row-level security
4. **Real-time Analytics**: DirectQuery vs Import decisions
5. **Collaboration**: Workspace management and sharing

### Key Areas to Emphasize
- Strong understanding of DAX and data modeling
- Experience with large datasets and performance optimization
- Knowledge of security and governance
- Ability to create compelling visualizations
- Understanding of Power BI ecosystem and licensing

Remember: Focus on practical examples and be ready to explain how you would solve real-world business problems using Power BI.